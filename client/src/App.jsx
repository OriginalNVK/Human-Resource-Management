import React from "react";
import Navbar from "./Components/Navbar";
import Content from "./Components/Content";
import Button from "./Components/Button";

function App() {
  return (
    <div className="flex">
      <Navbar />
      <div className="flex-1">
        <header className="bg-gray-900 text-white p-6 flex justify-between items-center">
          <div className="flex flex-col">
            <h1 className="text-xl">Dashboard/</h1>
            <h1 className="text-xl font-bold">Dashboard</h1>
          </div>
          <div className="flex items-center space-x-4">
            <div className="relative">
              <input
                className="bg-gray-800 text-white px-4 py-2 rounded-lg pl-10"
                type="text"
                placeholder="Search"
              />
              <span className="absolute left-3 top-2.5 text-white">🔍</span>
            </div>
            <Button icon="fas fa-user" color="blue" text="Sign In" />
            <select className="bg-gray-800 text-white px-4 py-2 rounded-lg">
              <option value="1">trannghiach2</option>
              <option value="2">Notification 2</option>
              <option value="3">Notification 3</option>
            </select>
            <Button icon="fas fa-setting" color="gray" text="Settings" />
          </div>
        </header>
        <Content />
      </div>
    </div>
  );
}

export default App;
