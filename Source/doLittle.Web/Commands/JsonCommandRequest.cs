/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Applications;
using doLittle.Lifecycle;

namespace doLittle.Web.Commands
{
    public class JsonCommandRequest
    {
        /// <summary>
        /// Initializes a new instance of <see cref="JsonCommandRequest"/>
        /// </summary>
        /// <param name="correlationId"><see cref="TransactionCorrelationId"/> for the transaction</param>
        /// <param name="type"><see cref="IApplicationResourceIdentifier">Identifier</see> of the command</param>
        /// <param name="content">Content of the command as Json</param>
        public JsonCommandRequest(TransactionCorrelationId correlationId, IApplicationResourceIdentifier type, string content)
        {
            CorrelationId = correlationId;
            Type = type;
            Content = content;
        }

        /// <summary>
        /// Gets the <see cref="TransactionCorrelationId"/> representing the transaction
        /// </summary>
        /// <returns>The <see cref="TransactionCorrelationId"/></returns>
        public TransactionCorrelationId CorrelationId { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationResourceIdentifier"/> representing the type of the Command
        /// </summary>
        /// <returns>
        /// <see cref="IApplicationResourceIdentifier"/> representing the type of the Command
        /// </returns>
        public IApplicationResourceIdentifier Type { get; }

        /// <summary>
        /// Gets the content of the command
        /// </summary>
        /// <returns>
        /// <see cref="string">Content</see> as Json
        /// </returns>
        public string Content { get; }
    }
}
