using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Pokedex.Models;

namespace Pokedex.Services
{
    public class FilterService
    {
        private readonly HttpClient _httpClient;

        public FilterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Pokemon>> GetPokemonsByTypeAsync(string typeName)
        {
            var response = await _httpClient.GetFromJsonAsync<TypeDetailResponse>($"https://pokeapi.co/api/v2/type/{typeName}");
            if (response?.Pokemon == null) return new List<Pokemon>();

            return response.Pokemon.Select(p => new Pokemon
            {
                Name = p.Pokemon.Name,
                ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{ExtractIdFromUrl(p.Pokemon.Url)}.png",
                Id = ExtractIdFromUrl(p.Pokemon.Url)
            }).ToList();
        }

        public async Task<List<Pokemon>> GetPokemonsByAbilityAsync(string abilityName)
        {
            var response = await _httpClient.GetFromJsonAsync<AbilityDetailResponse>($"https://pokeapi.co/api/v2/ability/{abilityName}");
            if (response?.Pokemon == null) return new List<Pokemon>();

            return response.Pokemon.Select(p => new Pokemon
            {
                Name = p.Pokemon.Name,
                ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{ExtractIdFromUrl(p.Pokemon.Url)}.png",
                Id = ExtractIdFromUrl(p.Pokemon.Url)
            }).ToList();
        }

        private int ExtractIdFromUrl(string url)
        {
            var segments = url.TrimEnd('/').Split('/');
            return int.Parse(segments.Last());
        }
    }

}
