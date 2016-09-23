angular.module('umbraco').directive('osChart', function (webapiService) {
    return {
        restrict: 'E',
        replace: true,
        templateUrl: '/App_Plugins/Analytics/js/directives/oschart/oschart.directive.html',
        scope: {
            result: '='
        },
        link: function (scope) {
            google.charts.load('current', {'packages':['corechart']});
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {

                var arrayData = [];

                webapiService.GetResult('/umbraco/backoffice/api/RealtimeAnalyticsApi/GetOperatingSystemInformation').then(function (response) {
                    arrayData.push(['Operating system', 'Users']);
                    for (var i = 0; i < response.length; i++) {
                        arrayData.push([response[i].OperatingSystem, response[i].ActiveUsers]);
                    }

                    var options = {
                        width: 900, height: 500,
                        pieSliceText: 'label'
                    };

                    var chart = new google.visualization.PieChart(document.getElementById('oschart'));

                    var data = google.visualization.arrayToDataTable(arrayData);
                    chart.draw(data, options);
                },
                function (message) {
                    
                });
            }
        }
    }
});