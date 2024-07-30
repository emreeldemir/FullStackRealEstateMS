import React from "react";

class CounterClass extends React.Component {
    constructor(props) {
        super(props);
        this.state = { count: 0 };
    }

    componentDidMount() {
        console.log("Component mounted");
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevState.count !== this.state.count) {
            console.log("Component updated");
        }
    }

    componentWillUnmount() {
        console.log("Component will umount");
    }

    render() {
        return (
            <div>
                <h1>Hello {this.props.name}</h1>
                <p>Count : {this.state.count}</p>
                <button onClick={() => this.setState({ count: this.state.count + 1 })}>
                    Increment
                </button>
            </div>
        );
    }
}

export default CounterClass;