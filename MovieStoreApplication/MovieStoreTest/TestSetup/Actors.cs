using System;
using MovieStoreApi.DBOperations;
using MovieStoreApi.Entities;

namespace MovieStoreApi.UnitTests.TestSetup
{
    public static class Actors
    {
        public static void AddActors(this MovieStoreDbContext Context)
        {
            Context.Actors.AddRange(

                    new Actor
                    {
                        Name = "Brad",
                        Surname = "Pitt"
                    },
                    new Actor
                    {
                        Name = "Johnny",
                        Surname = "Depp"
                    },
                    new Actor
                    {
                        Name = "Emma",
                        Surname = "Watson"
                    }
                    );
        }
    }
}

