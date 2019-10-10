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

	__webpack_require__(112);
	module.exports = __webpack_require__(112);


/***/ }),

/***/ 112:
/***/ (function(module, exports) {

	(function( window, undefined ) {
	    kendo.cultures["da-GL"] = {
	        name: "da-GL",
	        numberFormat: {
	            pattern: ["-n"],
	            decimals: 2,
	            ",": ".",
	            ".": ",",
	            groupSize: [3],
	            percent: {
	                pattern: ["-n %","n %"],
	                decimals: 2,
	                ",": ".",
	                ".": ",",
	                groupSize: [3],
	                symbol: "%"
	            },
	            currency: {
	                name: "Danish Krone",
	                abbr: "DKK",
	                pattern: ["-n $","n $"],
	                decimals: 2,
	                ",": ".",
	                ".": ",",
	                groupSize: [3],
	                symbol: "kr."
	            }
	        },
	        calendars: {
	            standard: {
	                days: {
	                    names: ["søndag","mandag","tirsdag","onsdag","torsdag","fredag","lørdag"],
	                    namesAbbr: ["søn.","man.","tir.","ons.","tor.","fre.","lør."],
	                    namesShort: ["sø","ma","ti","on","to","fr","lø"]
	                },
	                months: {
	                    names: ["januar","februar","marts","april","maj","juni","juli","august","september","oktober","november","december"],
	                    namesAbbr: ["jan","feb","mar","apr","maj","jun","jul","aug","sep","okt","nov","dec"]
	                },
	                AM: ["AM","am","AM"],
	                PM: ["PM","pm","PM"],
	                patterns: {
	                    d: "dd/MM/yyyy",
	                    D: "dddd 'den' d. MMMM yyyy",
	                    F: "dddd 'den' d. MMMM yyyy h.mm.ss tt",
	                    g: "dd/MM/yyyy h.mm tt",
	                    G: "dd/MM/yyyy h.mm.ss tt",
	                    m: "d. MMMM",
	                    M: "d. MMMM",
	                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
	                    t: "h.mm tt",
	                    T: "h.mm.ss tt",
	                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
	                    y: "MMMM yyyy",
	                    Y: "MMMM yyyy"
	                },
	                "/": "/",
	                ":": ".",
	                firstDay: 1
	            }
	        }
	    }
	})(this);


/***/ })

/******/ });