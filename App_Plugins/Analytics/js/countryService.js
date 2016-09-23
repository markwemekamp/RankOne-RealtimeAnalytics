angular.module('umbraco')
    .factory('countryService',
        function ($http) {
            var countries = [];

            $http.get('/App_Plugins/Analytics/data/countries.json').success(function (response) {
                countries = response;
            });

            function GetFullName(region) {
                var country = _.findWhere(countries, { alpha2: region });
                if (country != null) {
                    return country.name;
                }
                return null;
            }

            return {
                GetFullName: GetFullName
            };
        });