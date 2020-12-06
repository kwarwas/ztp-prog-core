using System;
using System.Dynamic;
using ImpromptuInterface;
using RepoDb;

namespace CQRS.ReadSide
{
    class NullLogger<TInterface> : DynamicObject where TInterface : class
    {
        public static TInterface Instance => new NullLogger<TInterface>().ActLike<TInterface>();

        public void AfterQuery(TraceLog log)
        {
            Console.WriteLine($"{log.ExecutionTime} {log.Statement}");
        }

        public void AfterQueryAll(TraceLog log)
        {
            Console.WriteLine($"{log.ExecutionTime} {log.Statement}");
        }
        
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Activator.CreateInstance(binder.ReturnType);
            return true;
        }
    }
}