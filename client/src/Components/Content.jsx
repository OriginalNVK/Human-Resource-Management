import React from "react";
import { dashboardContent } from "../Constants";

const Content = () => {
  return (
    <div className="flex-1 p-6 bg-gray-100">
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-6">
        {dashboardContent.map((item, index) => (
          <div className="bg-white p-6 rounded-lg shadow-md">
            <div className="flex items-center">
              <div className="p-3 bg-gray-200 rounded-full mr-4">
                <span>👥</span>
              </div>
              <div>
                <p className="text-gray-500 text-sm">{item.title}</p>
                <h2 className="text-2xl font-bold">{item.amount}</h2>
                <p
                  className={`${
                    item.change.startsWith("+")
                      ? "text-green-500"
                      : "text-red-500"
                  } text-sm`}
                >
                  {item.change}
                </p>
              </div>
            </div>
          </div>
        ))}
      </div>

      {/* Charts Section */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <div className="bg-white p-6 rounded-lg shadow-md">
          <h3 className="text-lg font-semibold mb-4">WEBSITE VIEW</h3>
          <p className="text-gray-500 text-sm mb-4">
            Last Campaign Performance
          </p>
          <div className="h-40 bg-gray-100 rounded-lg flex items-center justify-center">
            <p className="text-gray-500">[Chart Placeholder]</p>
          </div>
          <p className="text-gray-500 text-sm mt-4">campaign sent 2 days ago</p>
        </div>
        <div className="bg-white p-6 rounded-lg shadow-md">
          <h3 className="text-lg font-semibold mb-4">Website Views</h3>
          <p className="text-gray-500 text-sm mb-4">
            Last Campaign Performance
          </p>
          <div className="h-40 bg-gray-100 rounded-lg flex items-center justify-center">
            <p className="text-gray-500">[Chart Placeholder]</p>
          </div>
          <p className="text-gray-500 text-sm mt-4">updated 4 min ago</p>
        </div>
        <div className="bg-white p-6 rounded-lg shadow-md">
          <h3 className="text-lg font-semibold mb-4">COMPLETED TASKS</h3>
          <p className="text-gray-500 text-sm mb-4">
            Last Campaign Performance
          </p>
          <div className="h-40 bg-gray-100 rounded-lg flex items-center justify-center">
            <p className="text-gray-500">[Chart Placeholder]</p>
          </div>
          <p className="text-gray-500 text-sm mt-4">just updated</p>
        </div>
      </div>
    </div>
  );
};

export default Content;
