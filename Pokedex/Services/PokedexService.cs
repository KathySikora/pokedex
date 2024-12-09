using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
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

        public async Task<List<Pokemon>> GetPokemonsAsync(int startId, int count)
        {
            var pokemons = new List<Pokemon>();

            for (int id = startId; id < startId + count; id++)
            {
                try
                {
                    var response = await _httpClient.GetFromJsonAsync<PokemonResponse>($"https://pokeapi.co/api/v2/pokemon/{id}");
                    if (response != null)
                    {
                        pokemons.Add(new Pokemon
                        {
                            Name = response.Name,
                            ImageUrl = response.Sprites.FrontDefault
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching Pokémon with ID {id}: {ex.Message}");
                }
            }

            return pokemons;
        }
    }

}
