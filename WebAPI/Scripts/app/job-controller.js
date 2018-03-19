var app = angular.module('JobApp', []);

app.service('jobservice', function ($http) {
    this.getJobs = function () {
        var res = $http({
            method: 'GET',
            url: '/api/job'
        });
        return res;
    }
    this.getJob = function (filter, value) {
        var res;
        if (value.length === 0) {
            res = $http({
                method: 'GET',
                url: '/api/job'
            })
        }
        else {
            res = $http
                ({
                    method: 'GET',
                    url: '/api/job/' + value
                })
        }
        return res;
    }

    this.addBlankItem = function () {
        var res = { Id: 0, Title: "", Grade: ""};
        return res;
    }


    this.SaveItemToRepo = function (model) {
        var res;
        res = $http
            ({
                method: 'POST',
            url: '/api/job/',
                data: model
            })
        return res;
    }
});

app.controller('JobCtrl', function ($scope, $http, jobservice) {
    $scope.title = "loading jobs...";

    $scope.jobdata = [];
    $scope.idSearch = '';
    $scope.searchResults = [];
    $scope.filterValue = "";
    $scope.Message = '';
    $scope.newModel = {Id: 0, Title:"", Grade:""}
    loadJobs();
    function loadJobs() {
        var promise = jobservice.getJobs();

        promise.then(function (resp) {
            $scope.jobdata = resp.data;
            $scope.Message = "Call is Completed Successfully";
        }, function (err) {
            $scope.Message = "Call Failed " + err.status;
        });
    }

    $scope.getFilteredData = function () {
        var promise = jobservice.getJob('', $scope.filterValue);
        promise.then(function (resp) {
            $scope.jobdata = resp.data;
            $scope.Message = "Call is Completed Successfully";
        }, function (err) {
            $scope.Message = "Call Failed " + err.status;
        });
    };

    $scope.addBlankItem = function () {
        var promise = jobservice.addBlankItem();

        $scope.jobdata.push(promise);
        $scope.Message = "Call is Completed Successfully";
    };

    $scope.updateItemToRepo = function (index) {
        var recentModel = $scope.jobdata[index];
        var promise = jobservice.SaveItemToRepo(recentModel);
        promise.then(function (resp) {
            loadJobs();
        }, function (err) {
            $scope.Message = "Call Failed " + err.status;
        });
    }

    $scope.saveItemToRepo = function (index) {
        var recentModel = $scope.jobdata[index];
        var promise = jobservice.SaveItemToRepo(recentModel);
        promise.then(function (resp) {
            loadJobs();
        }, function (err) {
            $scope.Message = "Call Failed " + err.status;
        });
    }

});

