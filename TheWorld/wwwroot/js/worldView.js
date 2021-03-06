﻿// worldView.js

var width = 960,
    height = 800;
var color = d3.scale.category20();
var radius = d3.scale.sqrt()
    .range([0, 6]);
var bubbles = document.getElementById("bubbles");
var svg = d3.select(bubbles).append("svg")
    .attr("width", width)
    .attr("height", height);
var force = d3.layout.force()
    .size([width, height])
    .charge(-400)
    .linkDistance(function (d) { return radius(d.source.size) + radius(d.target.size) + 20; });
d3.json("/lib/_app/graph.json", function (error, graph) {
    if (error) throw error;
    force
        .nodes(graph.nodes)
        .links(graph.links)
        .on("tick", tick)
        .start();
    var link = svg.selectAll(".link")
        .data(graph.links)
        .enter().append("g")
        .attr("class", "link");
    link.append("line")
        .style("stroke-width", function (d) { return (d.bond * 2 - 1) * 2 + "px"; });
    link.filter(function (d) { return d.bond > 1; }).append("line")
        .attr("class", "separator");
    var node = svg.selectAll(".node")
        .data(graph.nodes)
        .enter().append("g")
        .attr("class", "node")
        .call(force.drag);
    node.append("circle")
        .attr("r", function (d) { return radius(d.size); })
        .style("fill", function (d) { return color(d.atom); });



    node.append("text")
        .append("a")
        .attr("dy", ".35em")
        .attr("text-anchor", "middle")
        .text(function (d) { return d.atom; })
        .attr("xlink:href", function (d) {
            return d.url;
        });

    function tick() {
        link.selectAll("line")
            .attr("x1", function (d) { return d.source.x; })
            .attr("y1", function (d) { return d.source.y; })
            .attr("x2", function (d) { return d.target.x; })
            .attr("y2", function (d) { return d.target.y; });
        node.attr("transform", function (d) { return "translate(" + d.x + "," + d.y + ")"; });
    }
});