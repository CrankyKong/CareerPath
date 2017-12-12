var width=960,height=800,color=d3.scale.category20(),radius=d3.scale.sqrt().range([0,6]),bubbles=document.getElementById("bubbles"),svg=d3.select(bubbles).append("svg").attr("width",width).attr("height",height),force=d3.layout.force().size([width,height]).charge(-400).linkDistance(function(t){return radius(t.source.size)+radius(t.target.size)+20});d3.json("/lib/_app/graph.json",function(t,e){function r(){n.selectAll("line").attr("x1",function(t){return t.source.x}).attr("y1",function(t){return t.source.y}).attr("x2",function(t){return t.target.x}).attr("y2",function(t){return t.target.y}),a.attr("transform",function(t){return"translate("+t.x+","+t.y+")"})}if(t)throw t;force.nodes(e.nodes).links(e.links).on("tick",r).start();var n=svg.selectAll(".link").data(e.links).enter().append("g").attr("class","link");n.append("line").style("stroke-width",function(t){return 2*(2*t.bond-1)+"px"}),n.filter(function(t){return t.bond>1}).append("line").attr("class","separator");var a=svg.selectAll(".node").data(e.nodes).enter().append("g").attr("class","node").call(force.drag);a.append("circle").attr("r",function(t){return radius(t.size)}).style("fill",function(t){return color(t.atom)}),a.append("text").append("a").attr("dy",".35em").attr("text-anchor","middle").text(function(t){return t.atom}).attr("xlink:href",function(t){return t.url})});