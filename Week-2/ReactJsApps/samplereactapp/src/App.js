import logo from './logo.svg';
import './App.css';
import ClassComponent from './components/ClassComponent';
import FunctionalComponent from './components/FunctionalComponent';
import CounterClass from './components/CounterClass';
import CounterFunctional from './components/CounterFunctional';


function App() {
  return (
    <div className="App">
      <header className="App-header">
        <CounterClass name="Mehmet" />
        <CounterFunctional name="Emre" />
      </header>
    </div>

  );
}

export default App;
