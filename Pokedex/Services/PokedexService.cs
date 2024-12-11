﻿using System.Net.Http.Json;
using Pokedex.Models;

namespace Pokedex.Services
{
    public class PokedexService
    {
        private readonly HttpClient _httpClient;

        public PokedexService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Pokemon>> GetPokemonsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<PokemonListResponse>("https://pokeapi.co/api/v2/pokemon?limit=2500&offset=0");
            if (response?.Results == null) return new List<Pokemon>();

            return response.Results.Select(pokemon =>
            {
                var id = ExtractIdFromUrl(pokemon.Url);
                return new Pokemon
                {
                    Name = pokemon.Name,
                    ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png",
                    Id = id
                };
            }).ToList();
        }

        public async Task<PokemonDetail> GetPokemonDetailsAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<PokemonDetailResponse>($"https://pokeapi.co/api/v2/pokemon/{id}");
            if (response == null) return null;

            return new PokemonDetail
            {
                Name = response.Name,
                Height = response.Height,
                Weight = response.Weight,
                Abilities = response.Abilities.Select(a => a.Ability.Name).ToList(),
                Types = response.Types.Select(t => t.Type.Name).ToList()
            };
        }

        public async Task<List<PokemonType>> GetTypesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<TypeListResponse>("https://pokeapi.co/api/v2/type?limit=25&offset=0");
            if (response?.Results == null) return new List<PokemonType>();

            return response.Results.Select(type => new PokemonType
            {
                Name = type.Name,
                Url = type.Url
            }).ToList();
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

        public async Task<List<PokemonAbility>> GetAbilitiesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<AbilityListResponse>("https://pokeapi.co/api/v2/ability?limit=400&offset=0");
            if (response?.Results == null) return new List<PokemonAbility>();

            return response.Results.Select(ability => new PokemonAbility
            {
                Name = ability.Name,
                Url = ability.Url
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

        public async Task<List<Pokemon>> GetAllPokemonsWithDetailsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<PokemonListResponse>("https://pokeapi.co/api/v2/pokemon?limit=2500&offset=0");
            if (response?.Results == null) return new List<Pokemon>();

            var pokemons = new List<Pokemon>();

            foreach (var result in response.Results)
            {
                var id = ExtractIdFromUrl(result.Url);
                var detailResponse = await _httpClient.GetFromJsonAsync<PokemonDetailResponse>($"https://pokeapi.co/api/v2/pokemon/{id}");

                if (detailResponse != null)
                {
                    pokemons.Add(new Pokemon
                    {
                        Id = id,
                        Name = detailResponse.Name,
                        ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png",
                        Types = detailResponse.Types.Select(t => t.Type.Name).ToList(),
                        Height = detailResponse.Height,
                        Weight = detailResponse.Weight
                    });
                }
            }

            return pokemons;
        }

        public async IAsyncEnumerable<List<Pokemon>> GetPokemonBatchesAsync(int batchSize = 100)
        {
            var response = await _httpClient.GetFromJsonAsync<PokemonListResponse>("https://pokeapi.co/api/v2/pokemon?limit=2500&offset=0");
            if (response?.Results == null) yield break;

            var pokemons = new List<Pokemon>();

            foreach (var (pokemon, index) in response.Results.Select((p, i) => (p, i)))
            {
                var id = ExtractIdFromUrl(pokemon.Url);
                var detailResponse = await _httpClient.GetFromJsonAsync<PokemonDetailResponse>($"https://pokeapi.co/api/v2/pokemon/{id}");

                if (detailResponse != null)
                {
                    pokemons.Add(new Pokemon
                    {
                        Id = id,
                        Name = detailResponse.Name,
                        ImageUrl = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png",
                        Types = detailResponse.Types.Select(t => t.Type.Name).ToList(),
                        Abilities = detailResponse.Abilities.Select(a => a.Ability.Name).ToList(),
                        Height = detailResponse.Height,
                        Weight = detailResponse.Weight
                    });
                }

                if ((index + 1) % batchSize == 0)
                {
                    yield return pokemons.ToList();
                    pokemons.Clear();
                }
            }

            if (pokemons.Any())
            {
                yield return pokemons;
            }
        }



        private int ExtractIdFromUrl(string url)
        {
            var segments = url.TrimEnd('/').Split('/');
            return int.Parse(segments.Last());
        }
    }
}
