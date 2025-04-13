import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Navbar from "./Components/Navbar";
import DBContent from "./Components/DBContent";
import Button from "./Components/Button";
import Profile from "./Pages/Profile";

function App() {
  return (
    <Router>
      <div className="flex">
        <Navbar />
        <div className="flex-1">
          <header className="bg-gray-900 text-white p-6 flex justify-between items-center">
            <div className="flex flex-col">
              <h1 className="text-xl">Dashboard/</h1>
              <h1 className="text-xl font-bold">Dashboard</h1>
            </div>
          </header>
          <Routes>
            <Route path="/" element={<DBContent />} />
            <Route path="/profile" element={<Profile />} />
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App;
