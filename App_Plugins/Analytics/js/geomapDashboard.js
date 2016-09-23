(function () {
    // Controller
    function rankOneRealtimeGeomap($scope, webapiService, countryService) {
        google.charts.load('current', { 'packages': ['geochart', 'corechart'] });
        google.charts.setOnLoadCallback(initialize);

        $scope.zoomedRegion = '';

        function renderGraph(response) {

            var arrayData = [];

            if ($scope.zoomedRegion == '') {
                arrayData.push(['Country', 'Visitors']);
                for (var i = 0; i < response.length; i++) {
                    arrayData.push([response[i].Country, response[i].ActiveUsers]);
                }
            } else {
                $scope.options.region = $scope.zoomedRegion;
                $scope.options.displayMode = 'markers';

                arrayData.push(['City', 'Visitors']);
                for (var i = 0; i < response.length; i++) {
                    arrayData.push([response[i].City, response[i].ActiveUsers]);
                }  
            }
            var data = google.visualization.arrayToDataTable(arrayData);
            $scope.chart.draw(data, $scope.options);
        }

        function refreshData() {
            var url = '';
            if ($scope.zoomedRegion == '') {
                url = '/umbraco/backoffice/api/RealtimeAnalyticsApi/GetAllCountries';
            } else {
                var country = countryService.GetFullName($scope.zoomedRegion);
                url = '/umbraco/backoffice/api/RealtimeAnalyticsApi/GetCountry?name=' + country;
            }

            webapiService.GetResult(url).then(function (response) {
                renderGraph(response);
            },
            function (message) {
                $scope.error = message;
            });

            webapiService.GetResult('/umbraco/backoffice/api/RealtimeAnalyticsApi/GetBrowserInformation')
                .then(function(response) {
                    console.log(response);
                });
        }

        function switchView(e) {
            $scope.zoomedRegion = e.region;
            refreshData();
        }

        function initialize() {
            $scope.chart = new google.visualization.GeoChart(document.getElementById('regions_div'));
            google.visualization.events.addListener($scope.chart, 'regionClick', switchView);

            $scope.options = { width: 900, height: 500, legend: 'none' };

            refreshData();
        }
    };

    // Register the controller
    angular.module("umbraco").controller('rankOneRealtimeGeomap', rankOneRealtimeGeomap);

})();