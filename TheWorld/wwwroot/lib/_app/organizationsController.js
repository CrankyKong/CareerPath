!function(){"use strict";function n(n){var a=this;a.organizations=[],a.newOrganization={},a.errorMessage="",a.isBusy=!0,n.get("/api/organizations").then(function(n){angular.copy(n.data,a.organizations),a.organizations.sort(function(n,a){var o=n.name.toLowerCase(),i=a.name.toLowerCase();return o>i?1:i>o?-1:0});for(var o={},i=0,r=a.organizations.length;r>i;i++)o[a.organizations[i].name]=a.organizations[i];a.organizations=new Array;for(var t in o)a.organizations.push(o[t])},function(){a.errorMessage="Failed to load data : "+error})["finally"](function(){a.isBusy=!1}),a.addOrganization=function(){a.isBusy=!0,a.errorMessage="",n.post("/api/organizations",a.newOrganization).then(function(n){a.organizations.push(n.data),a.newOrganization={}},function(){a.errorMessage="Failed to save new organization: "+error})["finally"](function(){a.isBusy=!1})}}n.$inject=["$http"],angular.module("app-trips").controller("organizationsController",n)}();