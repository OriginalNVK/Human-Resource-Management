import React from 'react'

const NavbarItem = ({url, icon, children}) => {
  return (
    <a
      href={`${url}`}
      className="flex items-center px-6 py-3 text-gray-600 hover:bg-gray-100 rounded-lg"
    >
          <span className="mr-3">{icon}</span> {children}
    </a>
  );
}

export default NavbarItem