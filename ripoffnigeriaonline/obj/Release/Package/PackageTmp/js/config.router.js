'use strict';

/**
 * Config for the router
 */
angular.module('app')
  .run(
    ['$rootScope', '$state', '$stateParams',
      function ($rootScope, $state, $stateParams) {
          $rootScope.$state = $state;
          $rootScope.$stateParams = $stateParams;
      }
    ]
  )
  .config(
    ['$stateProvider', '$urlRouterProvider', 'JQ_CONFIG', 'MODULE_CONFIG',
      function ($stateProvider, $urlRouterProvider, JQ_CONFIG, MODULE_CONFIG) {
          var layout = "tpl/app.html";
          if (window.location.href.indexOf("material") > 0) {
              layout = "tpl/blocks/material.layout.html";
              $urlRouterProvider
                .otherwise('/app/dashboard-v3');
          } else {
              $urlRouterProvider
                .otherwise('/app/ripoff');
          }

          $stateProvider
              .state('app', {
                  abstract: true,
                  url: '/app',
                  templateUrl: layout,
                  resolve: load(['moment', 'js/controllers/bootstrap.js'])
              })
              .state('admin', {
                  url: '/admin',
                  templateUrl: 'tpl/admin/admin.html',
                  resolve: load(['moment', 'js/controllers/bootstrap.js'])
              })
              .state('admin.dashboard', {
                  url: '/dashboard',
                  templateUrl: 'tpl/admin/dashboard.html',
                  resolve: load(['moment', 'js/controllers/bootstrap.js'])
              })

              .state('app.ripoff', {
                  url: '/ripoff',
                  templateUrl: 'tpl/ripoff.html',
                  controller: 'mainController',
                  resolve: load(['moment', 'js/app/index/ripoff.js'])
              })

              .state('app.thankyou', {
                  url: '/thank-You',
                  templateUrl: 'tpl/thankYou.html',
                  resolve: load(['moment', 'js/app/anonymous.js'])
              })
              .state('app.profile', {
                  url: '/user/{username}',
                  templateUrl: 'tpl/profile.html',
                  controller: 'UserProfileCtrl',
                  resolve: load(['moment', 'js/app/user/profile.js'])
              })
              .state('app.lawsuit', {
                  url: '/lawsuitl0{reportId}/{append}',
                  templateUrl: 'tpl/lawsuit.html',
                  controller: 'lawSuitCtrl'
              })
              .state('app.needlawyer', {
                  url: '/ineedalawyer',
                  templateUrl: 'tpl/needalawyer.html',
              })
              .state('app.settings', {
                  url: '/settings/{username}',
                  templateUrl: 'js/app/settings/settings.html',
                  controller: 'UserSettingsCtrl'
              })
              .state('app.tou', {
                  url: '/tou',
                  templateUrl: 'tpl/tou.html'
              })
              .state('app.disclaimer', {
                  url: '/disclaimer',
                  templateUrl: 'tpl/disclaimer.html'
              })
              .state('app.reportbug', {
                  url: '/reportbug',
                  templateUrl: 'tpl/reportBug.html',
                  controller: 'reportBugController'
              })
               .state('app.payment', {
                   url: '/payment',
                   templateUrl: 'tpl/payment.html',
                   controller: 'processPaymentCtrl'
               })
                .state('app.success', {
                    url: '/paymentsuccess',
                    templateUrl: 'tpl/psuccess.html',
                    controller: 'processPaymentCtrl'
                })
                .state('app.faq', {
                    url: '/faq',
                    templateUrl: 'tpl/docs.html',
                })
                .state('app.au', {
                    url: '/au',
                    templateUrl: 'tpl/au.html',
                    controller: 'contactController'
                })
                .state('app.privacy', {
                    url: '/privacy',
                    templateUrl: 'tpl/privacy.html',
                })
                .state('app.feedback', {
                    url: '/feedback',
                    templateUrl: 'tpl/feedback.html',
                    controller: 'feedbackController'
                })
                .state('app.reset', {
                    url: '/resetlink',
                    template: '<div ui-view class="fade-in-up panel"><div class="wrapper-lg"><h2>Password link sent</h2><p>Please check your email and click link to reset your password.</p></div></div>'
                })
              .state('app.page.search', {
                  url: '/search/:param',
                  templateUrl: 'tpl/search/search.html',
                  controller: 'SearchCtr',
                  resolve: load(['moment', 'js/app/search/search.js'])

              })
              .state('app.latest', {
                  url: '/latest',
                  template: '<div ui-view class="fade-in-up"></div>',
                  controller: 'latestReportCtrl',
                  resolve: load(['moment', 'js/app/report/latest.js'])
              })
              .state('app.report', {
                  url: '/report',
                  template: '<div ui-view class="fade-in-up"></div>',
                  resolve: load(['moment', 'js/app/report/detail.js'])
              })
                .state('app.form.update', {
                    url: '/myReports',
                    templateUrl: 'tpl/ureports.html',
                    controller: 'updateReportCtrl',
                    resolve: load(['js/app/report/update.js'])
                })
                .state('app.form.getlawyer', {
                    url: '/getLawyer/{reportId}/{append}',
                    templateUrl: 'tpl/getlawyer.html',
                    controller: 'getaLawyerCtrl',
                    resolve: load(['js/app/lawfirm/getLawyer.js'])
                })

                .state('app.invoice', {
                    url: '/invoice/:param/{append}',
                    templateUrl: 'tpl/invoice.html',
                    controller: 'invoiceLawyerCtrl',
                    resolve: load(['js/app/lawfirm/invoice.js'])
                })
                .state('app.process', {
                    url: '/prcoess/{clientId}/{append}',
                    templateUrl: 'tpl/process.html',
                    controller: 'processPaymentCtrl',
                    resolve: load(['js/app/payment/process.js'])
                })
             .state('app.latest.report', {
                 url: '/report',
                 templateUrl: 'tpl/latest.report.html',
                 controller: 'latestReportCtrl'
             })
             .state('app.report.detail', {
                 url: '/r0{reportId}/{append}',
                 templateUrl: 'tpl/report_detail.html',
                 controller: 'ReportDetailCtrl'
             })
             .state('app.form', {
                 url: '/form',
                 template: '<div ui-view class="fade-in"></div>',
                 resolve: load(['textAngular', 'js/app/report/report.js'])
             })
             .state('app.form.report', {
                 url: '/report',
                 templateUrl: 'tpl/form_report.html',
                 controller: 'FormReportCtrl',
                 resolve: load(['angularFileUpload', 'js/controllers/file-upload.js', 'js/app/report/report.js'])
             })
           .state('app.form.success', {
               url: '/success',
               templateUrl: 'tpl/reportsuccess.html',
           })
           .state('app.form.usuccess', {
               url: '/usuccess',
               templateUrl: 'tpl/updatesuccess.html',
           })
           .state('app.form.rebuttal', {
               url: '/rebuttal/{reportId}/{append}',
               templateUrl: 'tpl/rebuttal.html',
               controller: 'rebuttalFormCtrl',
               resolve: load(['angularFileUpload', 'js/controllers/file-upload.js', 'ui.select', 'js/controllers/select.js', 'js/app/rebuttal/rebuttal.js'])
           })
            .state('app.page', {
                url: '/page',
                template: '<div ui-view class="fade-in-down"></div>'
            })
            .state('resetpwd', {
                url: '/resetpwd/{id}/*token',
                templateUrl: 'tpl/resetPassword.html',
                resolve: load(['js/app/user/resetPwd.js'])
            })
            .state('app.associate', {
                url: '/associate',
                templateUrl: 'js/app/user/associate.html',
                controller: 'associateController'
            })
            .state('access', {
                url: '/access',
                template: '<div ui-view class="fade-in-right-big smooth"></div>',

            })
           .state('access.signin', {
               url: '/signin',
               templateUrl: 'tpl/page_signin.html',
               resolve: load(['js/app/user/signin.js'])
           })
           .state('access.admin', {
               url: '/admin',
               templateUrl: 'tpl/admin/secure.html',
               resolve: load(['js/admin/admin.js'])
           })
           .state('app.access', {
               template: '<div ui-view class="fade-in"></div>',
               resolve: load(['js/app/user/signin.js'])
           })
           .state('app.access.signup', {
               url: '/signup',
               templateUrl: 'tpl/signup.html',
               resolve: load(['ui.select', 'js/controllers/select.js', 'toaster', 'js/controllers/toaster.js', 'js/app/user/signup.js'])
           })
           .state('admin.register', {
               url: '/register',
               templateUrl: 'tpl/admin/register.html',
               resolve: load(['ui.select', 'js/controllers/select.js', 'toaster', 'js/controllers/toaster.js', 'js/admin/adminSignUp.js'])
           })
           .state('access.forgotpwd', {
               url: '/forgotpwd',
               templateUrl: 'tpl/page_forgotpwd.html'
           })
           .state('access.404', {
               url: '/404',
               templateUrl: 'tpl/page_404.html'
           })
              .state('admin.roles', {
                  url: '/roles',
                  templateUrl: 'tpl/admin/roles.html',
                  controller: 'RoleController'
              })
                .state('admin.assign', {
                    url: '/assign/:param/{append}',
                    templateUrl: 'tpl/admin/role.html',
                    resolve: load(['ui.select', 'js/controllers/select.js', 'toaster', 'js/controllers/toaster.js', 'js/admin/adminrole.js'])
                })
           .state('admin.client', {
               url: '/client',
               templateUrl: 'tpl/admin/client.html',
               controller: 'ClientController'
           })
           .state('admin.approval', {
               url: '/approval',
               templateUrl: 'tpl/admin/reports.html',
               controller: 'approvalReportCtrl',
               resolve: load(['moment'])
           })
           .state('admin.details', {
               url: '/r0{reportId}/{append}',
               templateUrl: 'tpl/admin/approve.html',
               controller: 'approveReportCtrl',
               resolve: load(['moment'])
           })


          function load(srcs, callback) {
              return {
                  deps: ['$ocLazyLoad', '$q',
                    function ($ocLazyLoad, $q) {
                        var deferred = $q.defer();
                        var promise = false;
                        srcs = angular.isArray(srcs) ? srcs : srcs.split(/\s+/);
                        if (!promise) {
                            promise = deferred.promise;
                        }
                        angular.forEach(srcs, function (src) {
                            promise = promise.then(function () {
                                if (JQ_CONFIG[src]) {
                                    return $ocLazyLoad.load(JQ_CONFIG[src]);
                                }
                                angular.forEach(MODULE_CONFIG, function (module) {
                                    if (module.name == src) {
                                        name = module.name;
                                    } else {
                                        name = src;
                                    }
                                });
                                return $ocLazyLoad.load(name);
                            });
                        });
                        deferred.resolve();
                        return callback ? promise.then(function () { return callback(); }) : promise;
                    }]
              }
          }


      }
    ]
  );
