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

	__webpack_require__(40);
	module.exports = __webpack_require__(40);


/***/ }),

/***/ 40:
/***/ (function(module, exports) {

	(function( window, undefined ) {
	    kendo.cultures["ar-TN"] = {
	        name: "ar-TN",
	        numberFormat: {
	            pattern: ["-n"],
	            decimals: 3,
	            ",": ",",
	            ".": ".",
	            groupSize: [3],
	            percent: {
	                pattern: ["-n%","n%"],
	                decimals: 3,
	                ",": ",",
	                ".": ".",
	                groupSize: [3],
	                symbol: "%"
	            },
	            currency: {
	                name: "Tunisian Dinar",
	                abbr: "TND",
	                pattern: ["-n $","n $"],
	                decimals: 3,
	                ",": ",",
	                ".": ".",
	                groupSize: [3],
	                symbol: "د.ت.‏"
	            }
	        },
	        calendars: {
	            standard: {
	                days: {
	                    names: ["الأحد","الإثنين","الثلاثاء","الأربعاء","الخميس","الجمعة","السبت"],
	                    namesAbbr: ["الأحد","الإثنين","الثلاثاء","الأربعاء","الخميس","الجمعة","السبت"],
	                    namesShort: ["ح","ن","ث","ر","خ","ج","س"]
	                },
	                months: {
	                    names: ["جانفييه","فيفرييه","مارس","أفريل","مي","جوان","جوييه","أوت","سبتمبر","أكتوبر","نوفمبر","ديسمبر"],
	                    namesAbbr: ["جانفييه","فيفرييه","مارس","أفريل","مي","جوان","جوييه","أوت","سبتمبر","أكتوبر","نوفمبر","ديسمبر"]
	                },
	                AM: ["ص","ص","ص"],
	                PM: ["م","م","م"],
	                patterns: {
	                    d: "dd-MM-yyyy",
	                    D: "dd MMMM, yyyy",
	                    F: "dd MMMM, yyyy H:mm:ss",
	                    g: "dd-MM-yyyy H:mm",
	                    G: "dd-MM-yyyy H:mm:ss",
	                    m: "dd MMMM",
	                    M: "dd MMMM",
	                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
	                    t: "H:mm",
	                    T: "H:mm:ss",
	                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
	                    y: "MMMM, yyyy",
	                    Y: "MMMM, yyyy"
	                },
	                "/": "-",
	                ":": ":",
	                firstDay: 1
	            }
	        }
	    }
	})(this);


/***/ })

/******/ });