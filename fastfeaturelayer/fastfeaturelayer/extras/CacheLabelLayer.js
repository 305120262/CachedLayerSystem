define([
    "dojo/_base/declare",
    "dojo/_base/array",
    "dojo/dom",
    "dojo/on",
    "dojo/request/xhr",
    "esri/Color",
    "dojo/_base/connect",
    "esri/SpatialReference",
    "esri/geometry/Point",
    "esri/geometry/Extent",
    "esri/graphic",
    "esri/symbols/SimpleMarkerSymbol",
    "esri/symbols/TextSymbol",
    "esri/symbols/SimpleFillSymbol",
    "esri/symbols/SimpleLineSymbol",
    "esri/dijit/PopupTemplate",
    "esri/layers/GraphicsLayer",
    "esri/request"
], function (
    declare, arrayUtils, dom, on,xhr, Color, connect,
    SpatialReference, Point, Extent, Graphic, SimpleMarkerSymbol, TextSymbol, SimpleFillSymbol, SimpleLineSymbol,
    PopupTemplate, GraphicsLayer, esriRequest
    ) {
    return declare(GraphicsLayer, {
        _self: null,
        url:null,
        size: 0,
        xmin: 0,
        ymin: 0,
        xmax: 0,
        ymax: 0,
        step: 0,
        metaloaded: 0,
        mode: "normal",
        buffer: 20,
        onMouseOverHandler:null,
        onMouseOutHandler:null,
        stack: [],
        hotspotdata: {},
        maxhit: 0,
        minhit:Infinity,
        constructor: function (options) {
            _self = this;
            _self.minScale = options.minScale;
            _self.maxScale = options.maxScale;
            _self.mode = options.mode;
            _self.onMouseOverHandler = options.mouseOverHanlder;
            _self.onMouseOverHandler = options.mouseOutHandler;
            _self.url = options.url;
            _self.buffer = options.buffer;
            var redis = xhr(_self.url + "/meta", {
                        handleAs: "json",
                        headers:{"X-Requested-With": null}
                    });
                   redis.then(
                            function (data) {
                                _self._sr= data.spatialReference || new SpatialReference({ "wkid": 102100 });
                                _self.size = parseFloat(data.size);
                                _self.xmin = parseFloat(data.xmin);
                                _self.xmax = parseFloat(data.xmax);
                                _self.ymin = parseFloat(data.ymin);
                                _self.ymax = parseFloat(data.ymax);
                                _self.step = parseFloat(data.step);
                                _self.metaloaded = 1;
                                if (_self.mode == "hotspot") {
                                    var hs = xhr(_self.url + "/hotspot", {
                                        handleAs: "json",
                                        headers: { "X-Requested-With": null }
                                    });
                                    hs.then(
                                        function (data) {
                                            arrayUtils.forEach(data.hotspot, function (d) {
                                                _self.hotspotdata[d.id] = d.hit;
                                                if (d.hit > _self.maxhit) {
                                                    _self.maxhit = d.hit;
                                                }
                                                if (d.hit < _self.minhit) {
                                                    _self.minhit = d.hit;
                                                }
                                            });
                                            
                                        },
                                        function (error) {
                                            console.log("Error: ", error.message);
                                        }
                                        );
                                }
                            },
                            function (error) {
                                console.log("Error: ", error.message);
                            }
                    );

        },
        _setMap: function (map, surface) {
            connect.connect(map, "onExtentChange", this, function (event) {
                if (this.metaloaded == 0) {
                    return;
                }
                if (map.getScale() < this.maxScale || map.getScale() > this.minScale) {
                    return;
                }
                for (var i = _self.graphics.length; i--;) {
                    var g = _self.graphics[i];
                    if (g.hasOwnProperty("isGrid") == true) {
                        if (g.isGrid == true) {
                            _self.remove(g);
                        }
                    }
                }
                var toremoved = [];
                while (_self.stack.length > _self.buffer) {
                    var oldgrid = this.stack.shift();
                    toremoved.push(oldgrid);
                }
                for (var i = _self.graphics.length; i--;) {
                    var g = _self.graphics[i];
                    if (g.hasOwnProperty("grid_id") == true) {
                        if (toremoved.indexOf(g.grid_id) != -1) {
                            _self.remove(g);
                        }
                    }
                }
                if (event.xmin > this.xmax) { return; }
                if (event.xmax < this.xmin) { return; }
                if (event.ymax < this.ymin) { return; }
                if (event.ymin > this.ymax) { return; }
                var start_x = Math.floor((event.xmin - this.xmin) / this.size);
                var start_y = Math.floor((event.ymin - this.ymin) / this.size);
                var end_x = Math.floor((event.xmax - this.xmin) / this.size);
                var end_y = Math.floor((event.ymax - this.ymin) / this.size);
                if (start_x < 0) { start_x = 0; }
                if (start_y < 0) { start_y = 0; }
                if (event.xmax >= this.xmax) { end_x = Math.ceil((this.xmax - this.xmin) / this.size) - 1; }
                if (event.ymax >= this.ymax) { end_y = Math.ceil((this.ymax - this.ymin) / this.size) - 1; }
                if (end_x - start_x > 10) { return; }
                if (end_y - start_y > 10) { return; }
                for (var g_y = start_y; g_y <= end_y; g_y++) {
                    for (var g_x = start_x; g_x <= end_x; g_x++) {
                        var grid_id = g_y * this.step + g_x;
                        //Draw Grid
                        if (_self.mode != "normal") {
                            var grid_op = 0.2;
                            if (_self.mode == "hotspot") {
                                if (_self.hotspotdata.hasOwnProperty(grid_id)) {
                                    var hit = _self.hotspotdata[grid_id];
                                    grid_op = (hit - _self.minhit) * 0.8 / (_self.maxhit - _self.minhit);
                                }
                                else {
                                    grid_op = 0;
                                }
                            }
                            var sfs = new SimpleFillSymbol(SimpleFillSymbol.STYLE_SOLID,
                                new SimpleLineSymbol(SimpleLineSymbol.STYLE_DASHDOT,
                                    new Color([255, 0, 0]), 2), new Color([255, 0, 0, grid_op])
                            );
                            var grid = new Extent(_self.xmin + g_x * _self.size, _self.ymin + g_y * _self.size, _self.xmin + (g_x + 1) * _self.size, _self.ymin + (g_y + 1) * _self.size, map.spatialReference);
                            var g = new Graphic(
                                grid,
                                sfs,
                                null,
                                null
                            );
                            g.isGrid = true;
                            _self.add(g);
                        }
                        if (_self.mode == "grid-only" || _self.mode == "hotspot")
                        { continue; }


                        //Request Cache
                        if (this.stack.indexOf(grid_id)!=-1) {
                            continue;
                        }
                        else {
                            this.stack.push(grid_id);
                        }
                        var redis = new xhr(_self.url+"/"+grid_id,{
                            handleAs: "json",
                            headers: { "X-Requested-With": null }
                        });
                        redis.grid_id = grid_id;
                        redis.then(function (data) {
                            arrayUtils.forEach(data.features, function (fea) {
                                var ts = new TextSymbol(fea.symbol);
                                
                                var g = new Graphic(new Point(fea.geometry.x, fea.geometry.y, map.spatialReference), ts, null, null);
                                g.isGrid = false;
                                g.grid_id = redis.grid_id;
                                if (_self.onMouseOverHandler != null) {
                                    on(g, "mouse-over", _self.onMouseOverHandler);
                                }
                                if (_self.onMouseOutHandler != null) {
                                    on( g,"mouse-out", _self.onMouseOutHandler);
                                }
                                _self.add(g);
                            });
                        },
                            function (error) {
                                console.log("Error: ", error.message);
                            });
                    }
                }

            });
            
            // GraphicsLayer will add its own listener here
            var div = this.inherited(arguments);
            return div;
        },
        _unsetMap: function () {
            this.inherited(arguments);
            connect.disconnect(this._extendChange);
        }
        
    });
});

