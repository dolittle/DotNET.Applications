using doLittle.DependencyInversion;
using doLittle.Entities;
using doLittle.Execution;
using doLittle.Tasks;
using Machine.Specifications;
using Moq;

namespace doLittle.Specs.Tasks.for_TaskRepository.given
{
    public class a_task_repository
    {
        protected static Mock<IEntityContext<TaskEntity>> entity_context;
        protected static TaskRepository repository;
        protected static Mock<IContainer> container;

        Establish context = () =>
        {
            entity_context = new Mock<IEntityContext<TaskEntity>>();
            container = new Mock<IContainer>();
            repository = new TaskRepository(entity_context.Object, container.Object);
        };
    }
}
