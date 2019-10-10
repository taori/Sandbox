module.exports =
/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};

/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {

/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;

/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};

/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);

/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;

/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}


/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;

/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;

/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";

/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ({

/***/ 0:
/***/ (function(module, exports, __webpack_require__) {

	__webpack_require__(750);
	module.exports = __webpack_require__(750);


/***/ }),

/***/ 750:
/***/ (function(module, exports) {

	(function( window, undefined ) {
	    kendo.cultures["tn-BW"] = {
	        name: "tn-BW",
	        numberFormat: {
	            pattern: ["-n"],
	            decimals: 2,
	            ",": " ",
	            ".": ".",
	            groupSize: [3],
	            percent: {
	                pattern: ["-n%","n%"],
	                decimals: 2,
	                ",": " ",
	                ".": ".",
	                groupSize: [3],
	                symbol: "%"
	            },
	            currency: {
	                name: "Botswanan Pula",
	                abbr: "BWP",
	                pattern: ["-$n","$n"],
	                decimals: 2,
	                ",": " ",
	                ".": ".",
	                groupSize: [3],
	                symbol: "P"
	            }
	        },
	        calendars: {
	            standard: {
	                days: {
	                    names: ["Tshipi","Mosopulogo","Labobedi","Laboraro","Labone","Labotlhano","Matlhatso"],
	                    namesAbbr: ["Tsh","Mos","Bed","Rar","Ne","Tla","Mat"],
	                    namesShort: ["Tsh","Mos","Bed","Rar","Ne","Tla","Mat"]
	                },
	                months: {
	                    names: ["Ferikgong","Tlhakole","Mopitlo","Moranang","Motsheganang","Seetebosigo","Phukwi","Phatwe","Lwetse","Diphalane","Ngwanatsele","Sedimonthole"],
	                    namesAbbr: ["Fer","Tlh","Mop","Mor","Mot","See","Phu","Pha","Lwe","Dip","Ngw","Sed"]
	                },
	                AM: ["AM","am","AM"],
	                PM: ["PM","pm","PM"],
	                patterns: {
	                    d: "yyyy-MM-dd",
	                    D: "yyyy MMMM d, dddd",
	                    F: "yyyy MMMM d, dddd HH:mm:ss",
	                    g: "yyyy-MM-dd HH:mm",
	                    G: "yyyy-MM-dd HH:mm:ss",
	                    m: "MMMM d",
	                    M: "MMMM d",
	                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
	                    t: "HH:mm",
	                    T: "HH:mm:ss",
	                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
	                    y: "yyyy MMMM",
	                    Y: "yyyy MMMM"
	                },
	                "/": "-",
	                ":": ":",
	                firstDay: 0
	            }
	        }
	    }
	})(this);


/***/ })

/******/ });