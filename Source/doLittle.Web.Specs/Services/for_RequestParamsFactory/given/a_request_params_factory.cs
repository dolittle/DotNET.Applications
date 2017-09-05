using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using doLittle.Logging;
using doLittle.Serialization;
using doLittle.Web.Services;
using Machine.Specifications;
using Moq;

namespace doLittle.Web.Specs.Services.for_RequestParamsFactory.given
{
    public class a_request_params_factory
    {
        protected const string FROM_QUERYSTRING = "from querystring";
        protected const string FROM_FORMS = "from forms";
        protected const string FROM_INPUTSTREAM = "from inputstream";
        protected const string FROM_COOKIES = "from cookies";
        protected const string FROM_SERVERVARIABLES = "from server variables";
        
        protected const string IN_ALL = "ALL";
        protected const string IN_QUERY_STRING_ONLY = "Querystring";
        protected const string IN_FORM_ONLY = "Form";
        protected const string IN_INPUT_STREAM_ONLY = "InputStream";
        protected const string IN_COOKIES_ONLY = "Cookies";
        protected const string IN_SERVER_VARIABLES_ONLY = "Server Variables";
        protected const string IN_FORMS_INPUT_STREAM_COOKIES_AND_SERVER_VARIABLES = "Forms; Input Stream; Cookies; Server Variables";
        protected const string IN_INPUT_STREAM_COOKIES_AND_SERVER_VARIABLES = "Input Stream; Cookies; Server Variables";
        protected const string IN_COOKIES_AND_SERVER_VARIABLES = "Cookies; Server Variables";

        protected static Mock<ISerializer> serializer_mock;
        protected static IRequestParamsFactory request_params_factory;
        protected static IDictionary<string, string> input_stream_params;
        protected static Mock<IHttpRequest> http_request_base_mock;
        
        Establish context = () =>
                                {
                                    var querystringParams = new NameValueCollection
                                                            {
                                                                {IN_ALL, FROM_QUERYSTRING},
                                                                {IN_QUERY_STRING_ONLY, FROM_QUERYSTRING}
                                                            };

                                    var formParams = new NameValueCollection
                                                            {
                                                                {IN_ALL, FROM_FORMS},
                                                                {IN_FORM_ONLY, FROM_FORMS},
                                                                {IN_FORMS_INPUT_STREAM_COOKIES_AND_SERVER_VARIABLES, FROM_FORMS }
                                                            };

                                    var inputStreamParams = new Dictionary<string,string>
                                                            {
                                                                {IN_ALL, FROM_INPUTSTREAM},
                                                                {IN_INPUT_STREAM_ONLY, FROM_INPUTSTREAM},
                                                                {IN_FORMS_INPUT_STREAM_COOKIES_AND_SERVER_VARIABLES, FROM_INPUTSTREAM },
                                                                {IN_INPUT_STREAM_COOKIES_AND_SERVER_VARIABLES,FROM_INPUTSTREAM}
                                                            };
                                                                
                                     var serverVariablesParams = new NameValueCollection
                                                            {
                                                                {IN_ALL, FROM_SERVERVARIABLES},
                                                                {IN_SERVER_VARIABLES_ONLY, FROM_SERVERVARIABLES},
                                                                {IN_FORMS_INPUT_STREAM_COOKIES_AND_SERVER_VARIABLES, FROM_SERVERVARIABLES },
                                                                {IN_INPUT_STREAM_COOKIES_AND_SERVER_VARIABLES,FROM_SERVERVARIABLES},
                                                                {IN_COOKIES_AND_SERVER_VARIABLES,FROM_SERVERVARIABLES}
                                                            };

                                    serializer_mock = new Mock<ISerializer>();
                                    serializer_mock.Setup(s => s.FromJson(typeof(Dictionary<string,string>), Moq.It.IsAny<string>(), null)).Returns(inputStreamParams);

                                    var input_stream = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes("mystring");
                                    http_request_base_mock = new Mock<IHttpRequest>();
                                    http_request_base_mock.Setup(r => r.QueryString).Returns(querystringParams);
                                    http_request_base_mock.Setup(r => r.Form).Returns(formParams);
                                    http_request_base_mock.Setup(r => r.InputStream).Returns(new MemoryStream(input_stream));
                                    
                                    request_params_factory = new RequestParamsFactory(serializer_mock.Object, Mock.Of<ILogger>());
                                };
        
        }
}