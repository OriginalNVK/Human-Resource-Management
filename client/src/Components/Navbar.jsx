import React, { useState } from "react";
import NavbarItem from "./NavbarItem";

const Navbar = () => {
  const [activeItem, setActiveItem] = useState("/");

  const handleItemClick = (url) => {
    setActiveItem(url);
  };

  return (
    <div className="flex h-screen">
      {/* Sidebar */}
      <div className="w-80 bg-white shadow-md">
        <div className="p-5 bg-gray-900 text-white flex items-center">
          <a href="/">
            <h1 className="text-2xl font-bold">EMPLOYEES MANAGEMENT</h1>
          </a>
        </div>
        <nav className="mt-6">
          <ul className="text-xl">
            <li className="mb-2">
              <NavbarItem
                url="/"
                icon="🏠"
                className={`${activeItem === "/" ? "bg-black text-white" : ""}`}
                onClick={() => handleItemClick("/")}
              >
                Dashboard
              </NavbarItem>
            </li>
            <li className="mb-2">
              <NavbarItem
                url="/employees"
                icon="👤"
                className={`${
                  activeItem === "/employees" ? "bg-black text-white" : ""
                }`}
                onClick={() => handleItemClick("/employees")}
              >
                Profile
              </NavbarItem>
            </li>
            <li className="mb-2">
              <NavbarItem
                url="/users"
                icon="📊"
                className={`${
                  activeItem === "/users" ? "bg-black text-white" : ""
                }`}
                onClick={() => handleItemClick("/users")}
              >
                Users
              </NavbarItem>
            </li>
            <li className="mb-2">
              <NavbarItem
                url="/notifications"
                icon="🔔"
                className={`${
                  activeItem === "/notifications" ? "bg-black text-white" : ""
                }`}
                onClick={() => handleItemClick("/notifications")}
              >
                Notifications
              </NavbarItem>
            </li>
          </ul>
        </nav>
        <div className="mt-10 px-6">
          <p className="text-sm font-semibold text-gray-500">AUTH PAGES</p>
          <ul className="mt-4 text-xl">
            <li className="mb-2">
              <NavbarItem
                url="/login"
                icon="🔑"
                className={`${
                  activeItem === "/login" ? "bg-black text-white" : ""
                }`}
                onClick={() => handleItemClick("/login")}
              >
                Sign In
              </NavbarItem>
            </li>
            <li className="mb-2">
              <NavbarItem
                url="/register"
                icon="📝"
                className={`${
                  activeItem == "/register" ? "bg-black text-white" : ""
                }`}
                onClick={() => handleItemClick("/register")}
              >
                Sign Up
              </NavbarItem>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Navbar;
