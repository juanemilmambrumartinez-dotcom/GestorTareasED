using System.Net.Http.Json;
using GestorTareasED.Web.Models;

namespace GestorTareasED.Web.Services;

public class ProjectApiService
{
    private readonly HttpClient _http;

    public ProjectApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProjectResponseDto>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<List<ProjectResponseDto>>("api/Projects");
        return result ?? new List<ProjectResponseDto>();
    }

    public async Task<ProjectResponseDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<ProjectResponseDto>($"api/Projects/{id}");
    }

    public async Task<HttpResponseMessage> CreateAsync(CreateProjectDto dto)
    {
        return await _http.PostAsJsonAsync("api/Projects", dto);
    }

    public async Task<HttpResponseMessage> UpdateAsync(int id, CreateProjectDto dto)
    {
        return await _http.PutAsJsonAsync($"api/Projects/{id}", dto);
    }

    public async Task<HttpResponseMessage> DeleteAsync(int id)
    {
        return await _http.DeleteAsync($"api/Projects/{id}");
    }
}