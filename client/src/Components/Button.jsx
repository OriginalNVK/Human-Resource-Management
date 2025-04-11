import React from "react";

const Button = ({ icon, color, text }) => {
  const colors = {
    blue: "bg-blue-500",
    red: "bg-red-500",
    green: "bg-green-500",
    yellow: "bg-yellow-500",
    purple: "bg-purple-500",
    pink: "bg-pink-500",
    gray: "bg-gray-500",
  };

  return (
    <button
      className={`${
        colors[color] || "bg-gray-500"
      } text-white px-4 py-2 rounded-lg`}
      type="button"
    >
      <i className={icon}></i> {text}
    </button>
  );
};

export default Button;
