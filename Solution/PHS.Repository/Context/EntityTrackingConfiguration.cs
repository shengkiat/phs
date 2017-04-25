
namespace PHS.Repository.Context
{
    using PHS.DB.Attributes;
    using System;
    using System.Linq;

    public static class EntityTrackingConfiguration
    {
        public static bool IsTrackingEnabled(Type entityType)
        {
            TrackChangesAttribute trackChangesAttribute =
                entityType.GetCustomAttributes(true).OfType<TrackChangesAttribute>().SingleOrDefault();
            bool value = trackChangesAttribute != null;

            return value;
        }
    }
}