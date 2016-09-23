angular.module('umbraco').directive('browserChart', function (webapiService) {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App_Plugins/Analytics/js/directives/browserchart/browserchart.directive.html',
        scope: {
            result: '='
        },
        link: function (scope) {
            google.charts.load('current', {'packages':['corechart']});
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {

                var arrayData = [];

                webapiService.GetResult('/umbraco/backoffice/api/RealtimeAnalyticsApi/GetBrowserInformation').then(function (response) {
                    arrayData.push(['Browser', 'Users']);
                    for (var i = 0; i < response.length; i++) {
                        arrayData.push([response[i].Browser, response[i].ActiveUsers]);
                    }

                    var options = {
                        width: 900, height: 500,
                        pieSliceText: 'label',
                    };

                    var chart = new google.visualization.PieChart(document.getElementById('piechart'));

                    var data = google.visualization.arrayToDataTable(arrayData);
                    chart.draw(data, options);
                },
                function (message) {
                    
                });
            }
        }
    }
});