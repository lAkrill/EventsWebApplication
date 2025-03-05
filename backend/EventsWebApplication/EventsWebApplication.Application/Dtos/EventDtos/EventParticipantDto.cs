public record EventParticipantDto
{
    public string Title { get; set; }
    public List<ParticipantEventDto> ParticipantEventDtos {  get; set; }
}
