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

	__webpack_require__(662);
	module.exports = __webpack_require__(662);


/***/ }),

/***/ 662:
/***/ (function(module, exports) {

	(function( window, undefined ) {
	    kendo.cultures["sg"] = {
	        name: "sg",
	        numberFormat: {
	            pattern: ["-n"],
	            decimals: 2,
	            ",": ".",
	            ".": ",",
	            groupSize: [3],
	            percent: {
	                pattern: ["-n%","n%"],
	                decimals: 2,
	                ",": ".",
	                ".": ",",
	                groupSize: [3],
	                symbol: "%"
	            },
	            currency: {
	                name: "",
	                abbr: "",
	                pattern: ["$-n","$n"],
	                decimals: 0,
	                ",": ".",
	                ".": ",",
	                groupSize: [3],
	                symbol: "FCFA"
	            }
	        },
	        calendars: {
	            standard: {
	                days: {
	                    names: ["Bikua-ôko","Bïkua-ûse","Bïkua-ptâ","Bïkua-usïö","Bïkua-okü","Lâpôsö","Lâyenga"],
	                    namesAbbr: ["Bk1","Bk2","Bk3","Bk4","Bk5","Lâp","Lây"],
	                    namesShort: ["Bk1","Bk2","Bk3","Bk4","Bk5","Lâp","Lây"]
	                },
	                months: {
	                    names: ["Nyenye","Fulundïgi","Mbängü","Ngubùe","Bêläwü","Föndo","Lengua","Kükürü","Mvuka","Ngberere","Nabändüru","Kakauka"],
	                    namesAbbr: ["Nye","Ful","Mbä","Ngu","Bêl","Fön","Len","Kük","Mvu","Ngb","Nab","Kak"]
	                },
	                AM: ["AM","am","AM"],
	                PM: ["PM","pm","PM"],
	                patterns: {
	                    d: "d/M/yyyy",
	                    D: "dddd d MMMM yyyy",
	                    F: "dddd d MMMM yyyy HH:mm:ss",
	                    g: "d/M/yyyy HH:mm",
	                    G: "d/M/yyyy HH:mm:ss",
	                    m: "d MMMM",
	                    M: "d MMMM",
	                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
	                    t: "HH:mm",
	                    T: "HH:mm:ss",
	                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
	                    y: "MMMM yyyy",
	                    Y: "MMMM yyyy"
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