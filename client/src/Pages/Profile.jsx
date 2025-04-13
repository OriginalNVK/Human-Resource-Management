import React from "react";
import { userInformation } from "../Constants";
import { avt } from "../Assets";

const ProfilePage = () => {
  return (
    <div className="m-8 border-2 p-6 bg-white rounded-lg shadow-md">
      {/* Header Section */}
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center mb-6 gap-4">
        <div className="flex items-center gap-4 w-full">
          {/* Avatar */}
          <img
            src={`${avt}`}
            alt="Profile"
            className="rounded-full w-20 h-20 sm:w-32 sm:h-32"
          />

          {/* Name and Title */}
          <div className="flex-1">
            <h2 className="text-xl sm:text-2xl font-semibold text-gray-700">
              {userInformation.common.Name}
            </h2>
            <p className="text-gray-600 text-sm sm:text-base">
              {userInformation.common.Role} | {userInformation.common.Address}
            </p>
          </div>
        </div>

        {/* Edit Button - Now properly aligned to the right */}
        <div className="w-full sm:w-auto flex justify-end">
          <button className="flex items-center gap-2 border border-gray-300 text-gray-600 px-4 py-2 rounded-full hover:bg-gray-50 transition-colors text-sm sm:text-base">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-4 w-4"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z"
              />
            </svg>
            <span>Edit</span>
          </button>
        </div>
      </div>

      <div className="border-t border-gray-300 my-4"></div>
      {/* Personal Information Section */}
      <div className="mb-8">
        <div className="flex justify-between items-center mb-4">
          <h3 className="text-xl font-semibold text-gray-700 mb-4">
            Personal Information
          </h3>
          <button className="flex items-center gap-1 border border-gray-300 text-gray-600 px-4 py-2 rounded-full hover:bg-gray-50 transition-colors">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-4 w-4"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z"
              />
            </svg>
            <span>Edit</span>
          </button>
        </div>

        <div className="grid grid-cols-2 gap-4 mb-4">
          <div>
            <p className="text-sm text-gray-500">First Name</p>
            <p className="font-medium text-gray-800">
              {userInformation.personal.firstName}
            </p>
          </div>
          <div>
            <p className="text-sm text-gray-500">Last Name</p>
            <p className="font-medium text-gray-800">
              {userInformation.personal.lastName}
            </p>
          </div>
        </div>

        <div className="grid grid-cols-2 gap-4 mb-4">
          <div>
            <p className="text-sm text-gray-500">Email address</p>
            <p className="font-medium text-gray-800">
              {userInformation.personal.email}
            </p>
          </div>
          <div>
            <p className="text-sm text-gray-500">Phone</p>
            <p className="font-medium text-gray-800">
              {userInformation.personal.phone}
            </p>
          </div>
        </div>

        <div className="mb-4">
          <p className="text-sm text-gray-500">Bio</p>
          <p className="font-medium text-gray-800">
            {userInformation.personal.Bio}
          </p>
        </div>

        <div className="border-t border-gray-300 my-4"></div>
      </div>

      {/* Address Section */}
      <div>
        <div className="flex justify-between items-center mb-4">
          <h3 className="text-xl font-semibold text-gray-700 mb-4">Address</h3>
          <button className="flex items-center gap-1 border border-gray-300 text-gray-600 px-4 py-2 rounded-full hover:bg-gray-50 transition-colors">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className="h-4 w-4"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                strokeWidth={2}
                d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.572L16.732 3.732z"
              />
            </svg>
            <span>Edit</span>
          </button>
        </div>

        <div className="grid grid-cols-2 gap-4 mb-4">
          <div>
            <p className="text-sm text-gray-500">Country</p>
            <p className="font-medium text-gray-800">
              {userInformation.Address.Country}
            </p>
          </div>
          <div>
            <p className="text-sm text-gray-500">City/State</p>
            <p className="font-medium text-gray-800">
              {userInformation.Address.City}
            </p>
          </div>
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div>
            <p className="text-sm text-gray-500">Postal Code</p>
            <p className="font-medium text-gray-800">
              {userInformation.Address.PostalCode}
            </p>
          </div>
          <div>
            <p className="text-sm text-gray-500">TAX ID</p>
            <p className="font-medium text-gray-800">
              {userInformation.Address.TaxID}
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProfilePage;
