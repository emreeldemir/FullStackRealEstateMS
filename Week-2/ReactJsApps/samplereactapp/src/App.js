import logo from './logo.svg';
import './App.css';
import ClassComponent from './components/ClassComponent';
import FunctionalComponent from './components/FunctionalComponent';
import CounterClass from './components/CounterClass';
import CounterFunctional from './components/CounterFunctional';
import UserList from './components/UserList';


function App() {
  return (
    <div className="App">
      <header className="App-header">
        <UserList />
      </header>
    </div>

  );
}

export default App;
