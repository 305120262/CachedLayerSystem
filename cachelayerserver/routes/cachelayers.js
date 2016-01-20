var express = require('express');
var router = express.Router();
var redis = require('redis');
var config = require('../config.js');

router.use(function(req, res, next) {  
    res.header('Access-Control-Allow-Origin', '*');
    next();
});

router.get('/:layer/meta', function (req, res, next) {
  var client = redis.createClient({ host: config.redisserver, port: config.redisport });
  var prefix = "poi:"+req.params.layer+":"
  var meta = new Object();
  client.get(prefix+"size", function (err, reply) { meta.size = reply.toString(); });
  client.get(prefix+"xmin", function (err, reply) { meta.xmin = reply.toString(); });
  client.get(prefix+"ymin", function (err, reply) { meta.ymin = reply.toString(); });
  client.get(prefix+"xmax", function (err, reply) { meta.xmax = reply.toString(); });
  client.get(prefix+"ymax", function (err, reply) { meta.ymax = reply.toString(); });
  client.get(prefix+"step", function (err, reply) {
    meta.step = reply.toString();
    res.json(meta); 
  });
});

router.get('/:layer/hotspot', function (req, res, next) {
  var prefix = "poi:"+req.params.layer+":"
  var client = redis.createClient({ host: config.redisserver, port: config.redisport });
  client.zrevrange(prefix + "hotspot",0,-1,"WITHSCORES", function (err, reply) {
        var hs=[];
        for(var i=0;i<reply.length;i=i+2) {
            hs.push({"id":parseInt(reply[i]),"hit":parseInt(reply[i+1])});
        }
        res.json({'hotspot':hs});
        client.quit();
      });
  
});

router.get('/:layer/:gid', function (req, res, next) {
  if (req.params.gid != undefined) {
      var prefix = "poi:"+req.params.layer+":"
      var nid = Math.floor(Math.random() * config.nodes.length);
      var client = redis.createClient({ host: config.nodes[nid], port: config.redisport });
      client.get(prefix + req.params.gid.toString(), function (err, reply) {
        res.set("Cache-Control","max-age=60");
        res.set("ETag",prefix+req.params.gid.toString());
        res.send(reply.toString());
        client.zincrby(prefix+"hotspot",1,req.params.gid,function(err,reply){
            client.quit();
        });     
      });   
    }
});



router.get('/list', function (req, res, next) {
   var prefix = "poi:"
   var client = redis.createClient({ host: config.redisserver, port: config.redisport });
   client.smembers(prefix + "caches", function (err, reply) {
        res.json({"layers":reply});
        client.quit();
   });
   
});

router.get('/', function(req, res, next) {   
   var prefix = "poi:"
   var client = redis.createClient({ host: config.redisserver, port: config.redisport });
   client.smembers(prefix + "caches", function (err, reply) {
        res.render('cachelayers', { "title": reply});
        client.quit();
   });
   
});



module.exports = router;