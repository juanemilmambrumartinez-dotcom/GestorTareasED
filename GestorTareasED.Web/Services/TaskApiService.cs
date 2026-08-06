using System.Net.Http.Json;
using GestorTareasED.Web.Models;

namespace GestorTareasED.Web.Services;

public class TaskApiService
{
    private readonly HttpClient _http;

    public TaskApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<TaskResponseDto>> GetAllAsync()
    {
        var result = await _http.GetFromJsonAsync<List<TaskResponseDto>>("api/Tasks");
        return result ?? new List<TaskResponseDto>();
    }

    public async Task<TaskResponseDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<TaskResponseDto>($"api/Tasks/{id}");
    }

    public async Task<HttpResponseMessage> CreateAsync(CreateTaskDto dto)
    {
        return await _http.PostAsJsonAsync("api/Tasks", dto);
    }

    public async Task<HttpResponseMessage> UpdateAsync(int id, CreateTaskDto dto)
    {
        return await _http.PutAsJsonAsync($"api/Tasks/{id}", dto);
    }

    public async Task<HttpResponseMessage> DeleteAsync(int id)
    {
        return await _http.DeleteAsync($"api/Tasks/{id}");
    }
}