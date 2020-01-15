

    function ToFriendlyUrl(part)
    {
        while (part.indexOf(" ") >= 0)
            part = part.replace(" ", "-");

        while (part.indexOf("/") >= 0)
            part = part.replace("/", "-");

        while (part.indexOf("&") >= 0)
            part = part.replace("&", "-");

        while (part.indexOf("--") >= 0)
            part = part.replace("--", "-");

        return part;
    }


    function loadCompetition() {
        var season = document.getElementById("season").options[document.getElementById("season").selectedIndex];
        var category = document.getElementById("category").options[document.getElementById("category").selectedIndex];
        var region = document.getElementById("region").options[document.getElementById("region").selectedIndex];

        window.location = "/Competities/" + season.value + "/" + category.value + "/" + region.value + "/" +
                                                ToFriendlyUrl(season.text) + "/" + category.text + "/" + ToFriendlyUrl(region.text);
    }


    function loadRegionRatings() {
        var region = document.getElementById("region").options[document.getElementById("region").selectedIndex];
        var category = document.getElementById("category").options[document.getElementById("category").selectedIndex];

        window.location = "/Ratings/Afdeling/" + region.value + "/" + ToFriendlyUrl(region.text) + "/" + category.value;
    }

    function loadSeasonRatings() {
        var season = document.getElementById("season").options[document.getElementById("season").selectedIndex];
        var category = document.getElementById("category").options[document.getElementById("category").selectedIndex];

        window.location = "/Ratings/Seizoen/" + season.value + "/" + ToFriendlyUrl(season.text) + "/" + category.value;
    }

    function setInput(searchBox)
    {				
	    if ( searchBox.value == "Bondsnummer, naam speler of vereniging" )
	    {
		    searchBox.value = "";
		    searchBox.style["color"] = "black";
		    searchBox.style["fontStyle"] = "normal";
	    }
    }

    function releaseInput(searchBox)
    {				
	    if ( searchBox.value == "" )
	    {
		    searchBox.style["color"] = "#aaa";
		    searchBox.style["fontStyle"] = "italic";
		    searchBox.value = "Bondsnummer, naam speler of vereniging";
	    }
    }


    function initSearchBox(evt) {
        var searchBox = document.getElementById("searchbox");

	    if (searchBox.value != "Bondsnummer, naam speler of vereniging") {
		    searchBox.style["color"] = "black";
		    searchBox.style["fontStyle"] = "normal";
	    }
    }


    function stickyHeader(evt) {
        var doc = document.documentElement;
        var top = (window.pageYOffset || doc.scrollTop) - (doc.clientTop || 0);

        var el = document.getElementById("menu");

        if (top > 140) {
            el.style["position"] = "fixed";
            el.style["top"] = "0px";
        }
        else {
            el.style["position"] = "absolute";
            el.style["top"] = "140px";
        }
    }

    if (window.addEventListener) {
        window.addEventListener("load", initSearchBox, false);
        window.addEventListener("scroll", stickyHeader, false);
    }
    else {
        window.attachEvent("onscroll", stickyHeader);
        window.attachEvent("onload", initSearchBox);
    }