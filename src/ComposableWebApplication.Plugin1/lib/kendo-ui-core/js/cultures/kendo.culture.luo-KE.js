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

	__webpack_require__(497);
	module.exports = __webpack_require__(497);


/***/ }),

/***/ 497:
/***/ (function(module, exports) {

	(function( window, undefined ) {
	    kendo.cultures["luo-KE"] = {
	        name: "luo-KE",
	        numberFormat: {
	            pattern: ["-n"],
	            decimals: 2,
	            ",": ",",
	            ".": ".",
	            groupSize: [3],
	            percent: {
	                pattern: ["-n%","n%"],
	                decimals: 2,
	                ",": ",",
	                ".": ".",
	                groupSize: [3],
	                symbol: "%"
	            },
	            currency: {
	                name: "Kenyan Shilling",
	                abbr: "KES",
	                pattern: ["-n$","n$"],
	                decimals: 2,
	                ",": ",",
	                ".": ".",
	                groupSize: [3],
	                symbol: "Ksh"
	            }
	        },
	        calendars: {
	            standard: {
	                days: {
	                    names: ["Jumapil","Wuok Tich","Tich Ariyo","Tich Adek","Tich Ang’wen","Tich Abich","Ngeso"],
	                    namesAbbr: ["JMP","WUT","TAR","TAD","TAN","TAB","NGS"],
	                    namesShort: ["JMP","WUT","TAR","TAD","TAN","TAB","NGS"]
	                },
	                months: {
	                    names: ["Dwe mar Achiel","Dwe mar Ariyo","Dwe mar Adek","Dwe mar Ang’wen","Dwe mar Abich","Dwe mar Auchiel","Dwe mar Abiriyo","Dwe mar Aboro","Dwe mar Ochiko","Dwe mar Apar","Dwe mar gi achiel","Dwe mar Apar gi ariyo"],
	                    namesAbbr: ["DAC","DAR","DAD","DAN","DAH","DAU","DAO","DAB","DOC","DAP","DGI","DAG"]
	                },
	                AM: ["AM","am","AM"],
	                PM: ["PM","pm","PM"],
	                patterns: {
	                    d: "dd/MM/yyyy",
	                    D: "dddd, d MMMM yyyy",
	                    F: "dddd, d MMMM yyyy HH:mm:ss",
	                    g: "dd/MM/yyyy HH:mm",
	                    G: "dd/MM/yyyy HH:mm:ss",
	                    m: "MMMM d",
	                    M: "MMMM d",
	                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
	                    t: "HH:mm",
	                    T: "HH:mm:ss",
	                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
	                    y: "MMMM yyyy",
	                    Y: "MMMM yyyy"
	                },
	                "/": "/",
	                ":": ":",
	                firstDay: 0
	            }
	        }
	    }
	})(this);


/***/ })

/******/ });