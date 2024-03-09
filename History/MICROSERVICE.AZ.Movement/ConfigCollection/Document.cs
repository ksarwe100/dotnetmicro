using MongoDB.Bson;

namespace MICROSERVICE.AZ.Movement.ConfigCollection;
public class Document : IDocument
{
    public ObjectId Id { get; set; }

    public DateTime CreatedAt => Id.CreationTime;

}

