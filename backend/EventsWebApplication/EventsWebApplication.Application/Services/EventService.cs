﻿using AutoMapper;
using EventsWebApplication.Application.Exceptions;
using EventsWebApplication.Application.Interfaces;
using EventsWebApplication.Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace EventsWebApplication.Application.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public EventService(IEventRepository eventRepository, IMapper mapper, IFileStorageService fileStorageService)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        public async Task<EventReadDto> GetEventByIdAsync(Guid id, CancellationToken ct = default)
        {
            var @event = await _eventRepository.GetEventByIdAsync(id, ct);
            if (@event == null)
            {
               throw new NotFoundException($"Event not fount with id: {id}");
            }

            return _mapper.Map<EventReadDto>(@event);
        }

        public async Task<List<EventReadDto>> GetEventsByTitleAsync(string title, 
            int page = 0, 
            int pageSize = 20, 
            CancellationToken ct = default)
        {
            if(page <1 || pageSize < 1)
            {
                throw new InvalidPaginationException("Page number and page size must be greater then 1");
            }

            var events = await _eventRepository.GetEventsByTitleAsync(title, page, pageSize, ct);
            return _mapper.Map<List<EventReadDto>>(events);
        }

        public async Task<EventParticipantDto> GetEventParticipantsByIdAsync(Guid id, CancellationToken ct = default)
        {
            var @event = await _eventRepository.GetEventByIdAsync(id, ct);
            if (@event == null)
            {
                throw new NotFoundException($"Event not fount with id: {id}");
            }

            return _mapper.Map<EventParticipantDto>(@event);
        }

        public async Task<List<EventReadDto>> GetAllEventsAsync(int page = 0, 
            int pageSize = 20, 
            CancellationToken ct = default)
        {
            if (page < 1 || pageSize < 1)
            {
                throw new InvalidPaginationException("Page number and page size must be greater then 1");
            }

            var events = await _eventRepository.GetEventsByPageAsync(page, pageSize, ct);
            return _mapper.Map<List<EventReadDto>>(events);
        }

        public async Task<EventReadDto> CreateEventAsync(EventCreateDto eventCreateDto, CancellationToken ct = default)
        {
            if (eventCreateDto == null)
            {
                throw new ArgumentNullException(nameof(eventCreateDto));
            }

            var existingEvent = await _eventRepository.GetEventByTitleAsync(eventCreateDto.Title, ct);
            if (existingEvent != null)
            {
                throw new NotUniqueException($"Event with title {eventCreateDto.Title} already exists");
            }

            var @event = _mapper.Map<Event>(eventCreateDto);

            if (eventCreateDto.Image != null) {
                var imagePath = await _fileStorageService.SaveFileAsync(eventCreateDto.Image, "EventImages");
                @event.ImagePath = imagePath;
            }

            await _eventRepository.AddEventAsync(@event, ct);
            return _mapper.Map<EventReadDto>(@event);
        }

        public async Task<EventReadDto> UpdateEventAsync(EventUpdateDto eventUpdateDto, CancellationToken ct = default)
        {
            if (eventUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(eventUpdateDto));
            }

            var existingEvent = await _eventRepository.GetEventByIdAsync(eventUpdateDto.Id, ct);
            if (existingEvent == null)
            {
                throw new NotFoundException($"Event not found with id: {eventUpdateDto.Id}");
            }

            var newEvent = _mapper.Map(eventUpdateDto, existingEvent);

            if (eventUpdateDto.RemoveImage && !string.IsNullOrEmpty(existingEvent.ImagePath))
            {
                await _fileStorageService.DeleteFile(existingEvent.ImagePath, "EventImages");
                newEvent.ImagePath = null;
            }

            if (eventUpdateDto.Image != null)
            {
                var imagePath = await _fileStorageService.SaveFileAsync(eventUpdateDto.Image, "EventImages");
                newEvent.ImagePath = imagePath;
            }

            await _eventRepository.UpdateEventAsync(newEvent, ct);
            return _mapper.Map<EventReadDto>(newEvent);
        }

        public async Task DeleteEventByIdAsync(Guid id, CancellationToken ct = default)
        {
            var deletedEvent = await _eventRepository.GetEventByIdAsync(id, ct);
            if (deletedEvent == null)
            {
                throw new NotFoundException($"Event not found with id: {id}");
            }

            await _eventRepository.DeleteEventAsync(id, ct);
        }

        public async Task<List<EventReadDto>> GetEventsByCriteriaAsync(EventFilterDto criteria,
            CancellationToken ct = default)
        {
            if (criteria.Page < 1 || criteria.PageSize < 1)
            {
                throw new InvalidPaginationException("Page number and page size must be greater then 1");
            }

            var eventList = await _eventRepository.GetEventsByCriteriaAsync(criteria.Date, 
                criteria.Location,
                criteria.CategoryId,
                criteria.SearchQuery,
                criteria.Page,
                criteria.PageSize,
                ct);
            
            if(eventList == null)
            {
                throw new NotFoundException($"Events not found with this filter");
            }

            return _mapper.Map<List<EventReadDto>>(eventList);
        }
    }
}
