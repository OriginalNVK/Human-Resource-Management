import React from "react";

const Navbar = () => {
  return (
    <div className="flex h-screen">
      {/* Sidebar */}
      <div className="w-80 bg-white shadow-md">
        <div className="p-5 bg-gray-900 text-white flex items-center">
          <a href="/">
            <h1 className="text-2xl font-bold">
              EMPLOYEES MANAGEMENT
            </h1>
          </a>
        </div>
        <nav className="mt-6">
          <ul className="text-xl">
            <li className="mb-2">
              <a
                href="#"
                className="flex items-center px-6 py-3 bg-gray-900 text-white rounded-lg"
              >
                <span className="mr-3">🏠</span> Dashboard
              </a>
            </li>
            <li className="mb-2">
              <a
                href="#"
                className="flex items-center px-6 py-3 text-gray-600 hover:bg-gray-100 rounded-lg"
              >
                <span className="mr-3">👤</span> Profile
              </a>
            </li>
            <li className="mb-2">
              <a
                href="#"
                className="flex items-center px-6 py-3 text-gray-600 hover:bg-gray-100 rounded-lg"
              >
                <span className="mr-3">📊</span> Tables
              </a>
            </li>
            <li className="mb-2">
              <a
                href="#"
                className="flex items-center px-6 py-3 text-gray-600 hover:bg-gray-100 rounded-lg"
              >
                <span className="mr-3">🔔</span> Notifications
              </a>
            </li>
          </ul>
        </nav>
        <div className="mt-10 px-6">
          <p className="text-sm font-semibold text-gray-500">AUTH PAGES</p>
          <ul className="mt-4 text-xl">
            <li className="mb-2">
              <a
                href="#"
                className="flex items-center px-6 py-3 text-gray-600 hover:bg-gray-100 rounded-lg"
              >
                <span className="mr-3">🔑</span> Sign In
              </a>
            </li>
            <li className="mb-2">
              <a
                href="#"
                className="flex items-center px-6 py-3 text-gray-600 hover:bg-gray-100 rounded-lg"
              >
                <span className="mr-3">📝</span> Sign Up
              </a>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Navbar;
