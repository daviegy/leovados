/// <reference path="angular.min.js" />
'use strict'
var myApp = angular.module("myApp", [])
myApp.controller("contactUsController", function ($scope, $http) {
    $scope.MessageSent2 = false;
    $scope.MessageNotSent2 = false
    var config = {
        headers: {
            'Content-Type': 'application/json',
        }
    }
    $scope.sendMessage = function () {
        alert("i am here")
        $http.post("/home/contactus",config).then(
            function successCallBack(response) {
                $scope.MessageSent2 = true;
                $scope.MessageNotSent2 = false
            }, function errorCallBack(response) {
                $scope.MessageSent2 = false;
                $scope.MessageNotSent2 = true;
            })
    }
})