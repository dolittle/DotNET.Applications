/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dolittle.Commands.for_CommandRequestToCommandConverter
{
    public class command_with_all_property_types : ICommand
    {
        public string a_string {  get; set; }
        public int an_integer {  get; set; }
        public double a_double { get; set; }
        public float a_float { get; set; }
        public Guid a_guid { get; set; }
        public string AStringWithPascalCasingOnTarget { get; set; }
        public string astringwithpascalcasingonsource { get; set; }
        
        public concept_as_guid a_concept { get; set; }
        public complex_type a_complex_type { get; set; }

        public IEnumerable<int> an_enumerable_of_integers { get; set; }

        public IEnumerable<double> an_enumerable_of_doubles { get; set; }

        public IEnumerable<float> an_enumerable_of_floats { get; set; }

        public IEnumerable<string> an_enumerable_of_strings { get; set; }
        public IEnumerable<Guid> an_enumerable_of_guids { get; set; }
        public IEnumerable<concept_as_guid> an_enumerable_of_concepts { get; set; }
        public IEnumerable<complex_type> an_enumerable_of_complex_types { get; set; }        
    }
}