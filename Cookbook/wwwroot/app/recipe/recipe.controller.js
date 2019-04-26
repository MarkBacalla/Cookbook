(function () {
    'use strict';

    angular
        .module('app')
        .controller('Recipe', Recipe);

    Recipe.$inject = ['dataservice'];

    function Recipe(dataservice) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'recipe';
        vm.newRecipe = '';        
        vm.data = dataservice.cache.recipe;        

        vm.addRecipe = addRecipe;
        vm.clickRecipe = clickRecipe;

        activate();

        function activate() {
            loadData();
        }

        function loadData() {
            return dataservice.getRecipes()
                .then(function (recipes) {

                    if (recipes !== null && recipes.length >= 1)
                        return recipes[0];

                    return null;
                })
                .then(loadRecipe);
        }

        function loadRecipe(recipe) {
            if (recipe !== null) {
                return dataservice.getRecipe(recipe.id);
            }
            return null;
        }

        function addRecipe() {        
            // preload added data, but will be refreshed 
            var recipeToAdd = { name: vm.newRecipe };
            vm.data.recipes.push(recipeToAdd);

            return dataservice.addRecipe(recipeToAdd)                
                .then(function() {
                    vm.newRecipe = '';
                });

        }

        function clickRecipe(recipe) {
            return dataservice.getRecipe(recipe.id);
        }
    }
})();
