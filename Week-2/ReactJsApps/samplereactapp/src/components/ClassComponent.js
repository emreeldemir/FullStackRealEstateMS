import React from "react";

class ClassComponent extends React.Component {
  render() {
    return <h1>Class Component, {this.props.name}</h1>;
    
  }
}

export default ClassComponent;