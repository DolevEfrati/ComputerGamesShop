!function () {
    var gradPie = {};

    var pie = d3.layout.pie().sort(null).value(function (d) { return d.value; });

    createGradients = function (defs, colors, r) {
        var gradient = defs.selectAll('.gradient')
            .data(colors).enter().append("radialGradient")
            .attr("id", function (d, i) { return "gradient" + i; })
            .attr("gradientUnits", "userSpaceOnUse")
            .attr("cx", "0").attr("cy", "0").attr("r", r).attr("spreadMethod", "pad");

        gradient.append("stop").attr("offset", "0%").attr("stop-color", function (d) { return d; });

        gradient.append("stop").attr("offset", "30%")
            .attr("stop-color", function (d) { return d; })
            .attr("stop-opacity", 1);

        gradient.append("stop").attr("offset", "70%")
            .attr("stop-color", function (d) { return "black"; })
            .attr("stop-opacity", 1);
    }

    gradPie.draw = function (id, data, cx, cy, r, text) {
        var gPie = d3.select("#" + id).append("g")
            .attr("transform", "translate(" + cx + "," + cy + ")");

        var x = d3.scale.linear()
            .domain([0, d3.max(data)])
            .range([0, 420]);

        var tooltip = d3.select("body")
                        .append("div")
                        .style("position", "absolute")
                        .style("z-index", "10")
                        .style("visibility", "hidden")
                        .style("background", "white")
                        .style("padding", "0 5px")
                        .text("a simple tooltip");

        createGradients(gPie.append("defs"), data.map(function (d) { return d.color; }), 2.5 * r);

       gPie.selectAll("path").data(pie(data))
            .enter().append("path").attr("fill", function (d, i) { return "url(#gradient" + i + ")"; })
            .attr("d", d3.svg.arc().outerRadius(r))
            .each(function (d) { this._current = d; }).style("width", function(d) { return x(d) + "px"; })
            .text(function(d) { return d.data.label; })
            .on("mouseover", function(d){tooltip.text(d.data.label); return tooltip.style("visibility", "visible");})
              .on("mousemove", function(){return tooltip.style("top", (d3.event.pageY-10)+"px").style("left",(d3.event.pageX+10)+"px");})
              .on("mouseout", function(){return tooltip.style("visibility", "hidden");})

       gPie.append("text")
        .attr("text-anchor", "middle") 
        .attr("y", cy) 
        .style("font-size", "16px") 
        .style("text-decoration", "underline")  
        .text(text);
    }

    gradPie.drawChartBar = function(id, data, cx, cy, text) {
        var margin = {top: 20, right: 20, bottom: 70, left: 40},
            width = 600 - margin.left - margin.right,
            height = 420 - margin.top - margin.bottom;
        var x = d3.scale.ordinal().rangeRoundBands([0, width], .05);

        var y = d3.scale.linear().range([height, 0]);

        var xAxis = d3.svg.axis()
            .scale(x)
            .orient("bottom");

        var yAxis = d3.svg.axis()
            .scale(y)
            .orient("left")
            .ticks(d3.max(data, function(d) { return d.count; }));

        var tooltip = d3.select("body")
                        .append("div")
                        .style("position", "absolute")
                        .style("z-index", "10")
                        .style("visibility", "hidden")
                        .style("background", "white")
                        .style("padding", "0 5px")
                        .text("a simple tooltip");

        var svg = d3.select("#" + id)
            .attr("width", width + margin.left + margin.right)
            .attr("height", height + margin.top + margin.bottom)
            .append("g")
            .attr("transform", 
                  "translate(" + margin.left + "," + margin.top + ")")

          x.domain(data.map(function(d) { return d.name; }));
          y.domain([0, d3.max(data, function(d) { return d.count; })]);

          svg.append("g")
              .attr("class", "x axis")
              .attr("transform", "translate(0," + height + ")")
              .call(xAxis)
            .selectAll("text")
              .style("text-anchor", "end")
              .attr("dx", "-.8em")
              .attr("dy", "-.55em")
              .attr("transform", "rotate(-90)" );

          svg.append("g")
              .attr("class", "y axis")
              .call(yAxis)
            .append("text")
              .attr("transform", "rotate(-90)")
              .attr("y", 6)
              .attr("dy", ".71em")
              .style("text-anchor", "end")
              .text(text + " - Count");

          svg.selectAll("bar")
              .data(data)
            .enter().append("rect")
              .style("fill", "steelblue")
              .attr("x", function(d) { return x(d.name); })
              .attr("width", x.rangeBand())
              .attr("y", function(d) { return y(d.count); })
              .attr("height", function(d) { return height - y(d.count); })
              .on("mouseover", function(d){tooltip.text(d.name); return tooltip.style("visibility", "visible");})
              .on("mousemove", function(){return tooltip.style("top", (d3.event.pageY-10)+"px").style("left",(d3.event.pageX+10)+"px");})
              .on("mouseout", function(){return tooltip.style("visibility", "hidden");});
    }

    gradPie.transition = function (id, data, r) {
        function arcTween(a) {
            var i = d3.interpolate(this._current, a);
            this._current = i(0);
            return function (t) { return d3.svg.arc().outerRadius(r)(i(t)); };
        }

        d3.select("#" + id).selectAll("path").data(pie(data))
            .transition().duration(750).attrTween("d", arcTween);
    }

    this.gradPie = gradPie; 
}();