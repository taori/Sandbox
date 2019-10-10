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

	__webpack_require__(463);
	module.exports = __webpack_require__(463);


/***/ }),

/***/ 463:
/***/ (function(module, exports) {

	(function( window, undefined ) {
	    kendo.cultures["ksh-DE"] = {
	        name: "ksh-DE",
	        numberFormat: {
	            pattern: ["-n"],
	            decimals: 2,
	            ",": " ",
	            ".": ",",
	            groupSize: [3],
	            percent: {
	                pattern: ["-n %","n %"],
	                decimals: 2,
	                ",": " ",
	                ".": ",",
	                groupSize: [3],
	                symbol: "%"
	            },
	            currency: {
	                name: "Euro",
	                abbr: "EUR",
	                pattern: ["-n $","n $"],
	                decimals: 2,
	                ",": " ",
	                ".": ",",
	                groupSize: [3],
	                symbol: "€"
	            }
	        },
	        calendars: {
	            standard: {
	                days: {
	                    names: ["Sunndaach","Moondaach","Dinnsdaach","Metwoch","Dunnersdaach","Friidaach","Samsdaach"],
	                    namesAbbr: ["Su.","Mo.","Di.","Me.","Du.","Fr.","Sa."],
	                    namesShort: ["Su","Mo","Di","Me","Du","Fr","Sa"]
	                },
	                months: {
	                    names: ["Jannewa","Fäbrowa","Määz","Aprell","Mäi","Juuni","Juuli","Oujoß","Septämber","Oktoober","Novämber","Dezämber"],
	                    namesAbbr: ["Jan.","Fäb.","Mäz.","Apr.","Mäi","Jun.","Jul.","Ouj.","Säp.","Okt.","Nov.","Dez."]
	                },
	                AM: ["v.m.","v.m.","V.M."],
	                PM: ["n.m.","n.m.","N.M."],
	                patterns: {
	                    d: "d. M. yyyy",
	                    D: "dddd, 'dä' d. MMMM yyyy",
	                    F: "dddd, 'dä' d. MMMM yyyy HH:mm:ss",
	                    g: "d. M. yyyy HH:mm",
	                    G: "d. M. yyyy HH:mm:ss",
	                    m: "d. MMMM",
	                    M: "d. MMMM",
	                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
	                    t: "HH:mm",
	                    T: "HH:mm:ss",
	                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
	                    y: "MMMM yyyy",
	                    Y: "MMMM yyyy"
	                },
	                "/": ". ",
	                ":": ":",
	                firstDay: 1
	            }
	        }
	    }
	})(this);


/***/ })

/******/ });