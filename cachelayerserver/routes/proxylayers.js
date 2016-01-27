var express = require('express');
var router = express.Router();
var http = require('http');

router.use(function(req, res, next) {  
    res.header('Access-Control-Allow-Origin', '*');
    next();
});

router.get('/:url', function (req, res, next) {
   // req.
});


module.exports = router;