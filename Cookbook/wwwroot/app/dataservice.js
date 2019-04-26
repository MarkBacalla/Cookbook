(function () {
    'use strict';

    angular
        .module('app')
        .factory('dataservice', dataservice);

    dataservice.$inject = ['$http', 'toastr'];

    function dataservice($http, toastr) {
        var cache = {
            recipe: {
                recipe: {},
                recipes: []
            },
            ingredient: { ingredients: [] }
        };

        var service = {
            getRecipes: getRecipes,
            getRecipe: getRecipe,
            addRecipe: addRecipe,
            addIngredient: addIngredient,
            resetIngredients: resetIngredients,
            updateIngredientAvailability: updateIngredientAvailability,
            refreshRecipe: getRecipe.bind(null, null),
            cache: cache
        };

        return service;

        function getRecipes() {
            return $http({
                method: 'GET',
                url: '/api/recipe'
            }).then(function (response) {
                setCacheRecipes(response.data);
                return response.data;
            }).catch(function (e) {
                toastr.error('Error Loading Recipes');
                throw e;
            });
        }

        function getRecipe(recipeId) {
            recipeId = recipeId || cache.recipe.recipe.id;

            return $http({
                method: 'GET',
                url: '/api/recipe/' + recipeId
            }).then(function (response) {                
                setCacheRecipe(response.data);
                setCacheIngredients(response.data.ingredients);
                return response.data;
            }).catch(function(e) {
                toastr.error('Error Loading Recipe');
                throw e;
            });
        }

        function addRecipe(recipe) {
            return $http({
                method: 'POST',
                url: '/api/recipe',
                data: recipe
            }).then(function (response) {
                setCacheRecipe(response.data);
                setCacheIngredients([]);
                return response.data;
            }).catch(function(e) {
                toastr.error('Error adding Recipe');
                throw e;
            }).finally(function() {
                getRecipes();
            });

        }

        function addIngredient(ingredient, recipeId) {
            recipeId = recipeId || cache.recipe.recipe.id;
            return $http({
                method: 'POST',
                url: '/api/ingredient',
                data: { recipeId: recipeId, name: ingredient }
            }).then(function (response) {
                addCacheIngredient({ name: ingredient }); // temporary addition before service refresh
                return response.data;
            }).catch(function (e) {
                toastr.error('Error Adding Ingredient');
                throw e;
            });
        }

        function updateIngredientAvailability(ingredientId, recipeId, isAvailable) {
            recipeId = recipeId || cache.recipe.recipe.id;
            return $http({
                method: 'PUT',
                url: '/api/ingredient',
                data: { recipeId: recipeId, ingredientId: ingredientId, isAvailable: isAvailable }
            }).then(function (response) {
                return response.data;
            }).catch(function (e) {
                toastr.error('Error Updating Availability');
                throw e;
            });
        }

        function resetIngredients(recipeId) {
            recipeId = recipeId || cache.recipe.recipe.id;
            return $http({
                method: 'POST',
                url: '/api/ingredient/ClearAvailability/' + recipeId
            }).catch(function (e) {
                toastr.error('Error Clearing Availability');
                throw e;
            });
        }




        // Cache update
        function setCacheRecipes(recipes) {
            cache.recipe.recipes = recipes;
        }

        function setCacheIngredients(ingredients) {
            cache.ingredient.ingredients = ingredients;
        }

        function addCacheIngredient(ingredient) {
            cache.ingredient.ingredients.push(ingredient);
        }

        function setCacheRecipe(recipe) {
            cache.recipe.recipe = recipe;
        }
    }
})();