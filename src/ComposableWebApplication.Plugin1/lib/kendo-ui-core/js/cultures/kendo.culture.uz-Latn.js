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

	__webpack_require__(783);
	module.exports = __webpack_require__(783);


/***/ }),

/***/ 783:
/***/ (function(module, exports) {

	(function( window, undefined ) {
	    kendo.cultures["uz-Latn"] = {
	        name: "uz-Latn",
	        numberFormat: {
	            pattern: ["-n"],
	            decimals: 2,
	            ",": " ",
	            ".": ",",
	            groupSize: [3],
	            percent: {
	                pattern: ["-n%","n%"],
	                decimals: 2,
	                ",": " ",
	                ".": ",",
	                groupSize: [3],
	                symbol: "%"
	            },
	            currency: {
	                name: "",
	                abbr: "",
	                pattern: ["-n $","n $"],
	                decimals: 0,
	                ",": " ",
	                ".": ",",
	                groupSize: [3],
	                symbol: "soʻm"
	            }
	        },
	        calendars: {
	            standard: {
	                days: {
	                    names: ["yakshanba","dushanba","seshanba","chorshanba","payshanba","juma","shanba"],
	                    namesAbbr: ["Ya","Du","Se","Ch","Pa","Ju","Sh"],
	                    namesShort: ["Ya","Du","Se","Ch","Pa","Ju","Sh"]
	                },
	                months: {
	                    names: ["Yanvar","Fevral","Mart","Aprel","May","Iyun","Iyul","Avgust","Sentabr","Oktabr","Noyabr","Dekabr"],
	                    namesAbbr: ["Yanv","Fev","Mar","Apr","May","Iyun","Iyul","Avg","Sen","Okt","Noya","Dek"]
	                },
	                AM: ["TO","to","TO"],
	                PM: ["TK","tk","TK"],
	                patterns: {
	                    d: "dd/MM/yyyy",
	                    D: "dddd, yyyy MMMM dd",
	                    F: "dddd, yyyy MMMM dd HH:mm:ss",
	                    g: "dd/MM/yyyy HH:mm",
	                    G: "dd/MM/yyyy HH:mm:ss",
	                    m: "d-MMMM",
	                    M: "d-MMMM",
	                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
	                    t: "HH:mm",
	                    T: "HH:mm:ss",
	                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
	                    y: "MMMM, yyyy",
	                    Y: "MMMM, yyyy"
	                },
	                "/": "/",
	                ":": ":",
	                firstDay: 1
	            }
	        }
	    }
	})(this);


/***/ })

/******/ });