// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Dolittle.Machine.Specifications.MongoDB.given;
using Machine.Specifications;

namespace Dolittle.Machine.Specifications.MongoDB
{
    [Subject(typeof(a_mongo_db_instance))]
    public class when_exporting_a_collection : a_mongo_db_instance
    {
        static readonly string temp_file = Path.GetTempPath() + "export.json";
        static readonly string collection_name = typeof(ADocument).FullName;

        static IEnumerable<ADocument> read_from_export;

        Establish context = () =>
        {
            var firstDoc = new ADocument
            {
                Id = Guid.NewGuid(),
                AString = "Old woman",
                AnInt = 42,
                ADateTimeUTC = DateTime.UtcNow,
                ADateTimeLocal = DateTime.Now,
                ADateTimeOffset = DateTimeOffset.UtcNow,
                AnArrayOfStrings = new[]
                {
                    "strange", "women", "lying", "in", "ponds", "distributing", "swords", "is", "no", "basis", "for",
                    "a", "system", "of", "government"
                },
            };

            var secondDoc = new ADocument
            {
                Id = Guid.NewGuid(),
                AString = "Man!",
                AnInt = 667,
                ADateTimeUTC = DateTime.UtcNow,
                ADateTimeLocal = DateTime.Now,
                ADateTimeOffset = DateTimeOffset.UtcNow,
                AnArrayOfStrings = new[]
                {
                    "supreme", "executive", "power", "derives", "from", "a", "mandate", "from", "the", "masses"
                },
            };

            var thirdDoc = new ADocument
            {
                Id = Guid.NewGuid(),
                AString = "Denis",
                AnInt = 204,
                ADateTimeUTC = DateTime.UtcNow,
                ADateTimeLocal = DateTime.Now,
                ADateTimeOffset = DateTimeOffset.UtcNow,
                AnArrayOfStrings = new[]
                {
                    "not", "some", "farcical", "aquatic", "ceremony"
                },
            };

            var collection = database.GetCollection<ADocument>(collection_name);
            collection.InsertMany(new[] { firstDoc, secondDoc, thirdDoc });
        };

        Because of = () =>
        {
            Export<ADocument>(temp_file, collection_name);
            var exported_contents = File.ReadAllLines(temp_file);
            Thread.Sleep(500);
            read_from_export = ParseFile<ADocument>(temp_file);
        };

        It should_have_exported_the_file = () => read_from_export.Count().ShouldEqual(3);

        Cleanup the_file = () => File.Delete(temp_file);
    }
}