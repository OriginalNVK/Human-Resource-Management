import React, {useState} from "react";
import NavbarItem from "./NavbarItem";

const Navbar = () => {
  const [isOpen, setIsOpen] = useState(false);
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
              <NavbarItem url="/" icon ="🏠">Dashboard</NavbarItem>
            </li>
            <li className="mb-2">
              <NavbarItem url="/employees" icon ="👤">Profile</NavbarItem>
            </li>
            <li className="mb-2">
              <NavbarItem url="/users" icon ="📊">Users</NavbarItem>
            </li>
            <li className="mb-2">
              <NavbarItem url="/notifications" icon ="🔔">Notifications</NavbarItem>
            </li>
          </ul>
        </nav>
        <div className="mt-10 px-6">
          <p className="text-sm font-semibold text-gray-500">AUTH PAGES</p>
          <ul className="mt-4 text-xl">
            <li className="mb-2">
              <NavbarItem url="/login" icon ="🔑">Sign In</NavbarItem>
            </li>
            <li className="mb-2">
              <NavbarItem url="/register" icon ="📝">Sign Up</NavbarItem>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Navbar;
