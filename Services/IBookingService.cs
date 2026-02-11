using Backend.DTOs;

namespace Backend.Services;

public interface IBookingService
{
    Task<IEnumerable<BookingReadDto>> GetAllBookingsAsync();
    Task<BookingReadDto?> GetBookingByIdAsync(int id);
    Task<IEnumerable<BookingReadDto>> GetBookingsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<BookingReadDto>> GetBookingsByStatusAsync(int statusId);
    Task<IEnumerable<BookingReadDto>> GetBookingsByUserAsync(int userId);
    Task<IEnumerable<BookingReadDto>> GetBookingsByRoomAsync(int roomId);
    Task<IEnumerable<BookingReadDto>> GetBookingsByDateAndRoomAsync(DateTime startDate, DateTime endDate, int roomId);
    Task<(bool Success, string? ErrorMessage, BookingReadDto? Booking)> CreateBookingAsync(BookingCreateDto dto);
    Task<(bool Success, string? ErrorMessage)> UpdateBookingAsync(int id, BookingUpdateDto dto);
    Task<(bool Success, string? ErrorMessage)> DeleteBookingAsync(int id);
    Task<(bool Success, string? ErrorMessage)> UpdateBookingStatusAsync(int id, BookingStatusUpdateDto dto);
}
