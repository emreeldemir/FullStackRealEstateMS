
import BookList from './components/BookList';
import HomePage from './components/HomePage';
import AddBook from './components/AddBook';
import UpdateBook from './components/UpdateBook';
import DeleteBook from './components/DeleteBook';
import { BrowserRouter, Route, Routes } from 'react-router-dom';


function App() {
  return (
    <div className="App">
      <header className="App-header">
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/get" element={<BookList />} />
            <Route path="/post" element={<AddBook />} />
            <Route path="/put" element={<UpdateBook />} />
            <Route path="/delete" element={<DeleteBook />} />
          </Routes>
        </BrowserRouter>
      </header>
    </div>
  );
}
export default App;
