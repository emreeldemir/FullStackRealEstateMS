import React, { useState, useEffect } from "react";

function CounterFunction(props) {
    const [count, setCount] = useState(0);

    useEffect(() => {
        console.log("Component mounted");

        return () => {
            console.log("Component will unmount");
        };
    }, []);

    useEffect(() => {
        document.title = count;
        if (count > 0) console.log("Component updated");
    }, [count]);

    return (
        <div>
            <h1>Hello {props.name}</h1>
            <p>Count: {count}</p>
            <button onClick={() => setCount(count + 1)}>Increment</button>
        </div>
    );
}

export default CounterFunction;
