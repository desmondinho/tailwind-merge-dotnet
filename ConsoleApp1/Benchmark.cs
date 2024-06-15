using BenchmarkDotNet.Attributes;

using TailwindMerge.Common;

namespace TvojaZhopa;

[MemoryDiagnoser]
public class Benchmark
{
    [Params( 1000 )]
    public int Iterations;

    [Benchmark]
    public void Test1()
    {
        List<ClassGroup2> classGroups = [];

        for( var i = 0; i < Iterations; i++ )
        {
            classGroups.Add( new ClassGroup2( $"id-{i}", $"baseClassName-{i}", new ClassDefinition( "auto", "square", "video", Validators.IsArbitraryValue ) ) );
        }
    }

    [Benchmark]
    public void Test2()
    {
        List<ClassGroup2> classGroups = [];

        for( var i = 0; i < Iterations; i++ )
        {
            classGroups.Add( new ClassGroup2( $"id-{i}", $"baseClassName-{i}", new ClassDefinition( ["auto", "square", "video"], [Validators.IsArbitraryValue] ) ) );
        }
    }

    public readonly record struct ClassGroup2(
        string Id,
        string? BaseClassName,
        object[]? Definitions,
        ClassDefinition? Definition )
    {
        public ClassGroup2( string id, string baseClassName, ClassDefinition definition )
            : this( id, baseClassName, null, definition ) { }

        public ClassGroup2( string id, string baseClassName, object[] definitions )
            : this( id, baseClassName, definitions, null ) { }
    }

    public class ClassDefinition
    {
        private readonly List<string> _classNameParts = [];
        private readonly List<Func<string, bool>> _validators = [];
        private readonly object[] _definitions = [];

        public IEnumerable<string> ClassNameParts => _classNameParts.AsEnumerable();
        public IEnumerable<Func<string, bool>> Validators => _validators.AsEnumerable();

        public ClassDefinition( params object[] values )
        {
            //_definitions = values;
            Add( values );
        }

        public ClassDefinition( string[] classNameParts, Func<string, bool>[] validators )
        {
            _classNameParts.AddRange(classNameParts);
            _validators.AddRange( validators );
        }

        public void Add( params object[] values )
        {
            foreach( var value in values )
            {
                if( value is string @string )
                {
                    _classNameParts.Add( @string );
                }
                else if( value is Func<string, bool> func )
                {
                    _validators.Add( func );
                }
                else if( value is ClassDefinition definition )
                {
                    _validators.AddRange( definition.Validators );
                    _classNameParts.AddRange( definition.ClassNameParts );
                }
            }
        }
    }
}