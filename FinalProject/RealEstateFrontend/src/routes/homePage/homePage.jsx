import { useContext } from "react";
import SearchBar from "../../components/searchBar/SearchBar";
import "./homePage.scss";
import { AuthContext } from "../../context/AuthContext";
import { DropdownContextProvider } from "../../context/DropdownContext";

function HomePage() {

  const { currentUser } = useContext(AuthContext)

  return (
    <div className="homePage">
      <div className="textContainer">
        <div className="wrapper">
          <h1 className="title">Find Real Estate & Get Your Dream Place</h1>
          <p>
            OBSS .NET CodeCamp 2024 Final Project (Real Estate Management System)
          </p>
          <DropdownContextProvider>
            <SearchBar />
          </DropdownContextProvider>
          <div className="boxes">
            <div className="box">
              <h1>4+</h1>
              <h2>Years of Experience</h2>
            </div>
            <div className="box">
              <h1>23</h1>
              <h2>Award Gained</h2>
            </div>
            <div className="box">
              <h1>500+</h1>
              <h2>Property Ready</h2>
            </div>
          </div>
        </div>
      </div>
      <div className="imgContainer">
        <img src="/bg.png" alt="" />
      </div>
    </div>
  );
}

export default HomePage;
