using Zenject;

public class FooSignal : Signal<FooSignal, FooSignal.Args, FooSignal.Args2>
{

    public class Args
    {

        public int Value
        {
            get;
        }

        public Args(int value)
        {
            Value = value;
        }

    }

    public struct Args2
    {

        public int Value
        {
            get;
        }

        public Args2(int value)
        {
            Value = value;
        }

    }

}